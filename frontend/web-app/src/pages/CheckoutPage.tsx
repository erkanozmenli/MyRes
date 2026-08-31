import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { createSignalRConnection } from "../signalr";
import type { HubConnection } from "@microsoft/signalr";
import { startCheckout } from "../api/checkout";
import "../styles/CheckoutPage.css";


type CheckoutStatus =
    | "Ready"
    | "Processing"
    | "Completed"
    | "Failed";


interface NotificationMessage {

    id: string;

    tripId: string;

    type: number;

    message: string;

    createdAtUtc: string;
}


export function CheckoutPage() {

    const { tripId } = useParams();


    const [status, setStatus] =
        useState<CheckoutStatus>("Ready");


    const [notification, setNotification] =
        useState<NotificationMessage | null>(null);



    useEffect(() => {

        let connection: HubConnection;


        async function connect() {

            connection = createSignalRConnection();


            connection.on(
                "checkoutNotification",
                (message: NotificationMessage) => {


                    console.log(
                        "Notification received",
                        message
                    );


                    if (
                        message.tripId !== tripId
                    ) {
                        return;
                    }


                    setNotification(message);


                    switch(message.type) {

                        case 1:
                            setStatus("Completed");
                            break;


                        case 2:
                            setStatus("Failed");
                            break;


                        default:
                            console.warn(
                                "Unknown notification type",
                                message.type
                            );

                    }

                }
            );


            console.log(
                "Creating SignalR connection..."
            );


            try {

                await connection.start();


                console.log(
                    "SignalR connected. State:",
                    connection.state
                );

            }
            catch(err) {

                console.error(
                    "SignalR start failed",
                    err
                );

            }

        }


        connect().catch(console.error);



        return () => {

            if(connection) {

                connection.stop();

            }

        };


    }, [tripId]);




    const handleCheckout = async () => {

        if(!tripId)
            return;


        try {

            setStatus("Processing");

            await startCheckout(tripId);

        }
        catch {

            setStatus("Failed");

            setNotification({
                id: crypto.randomUUID(),
                tripId,
                type: 2,
                message: "Checkout request failed.",
                createdAtUtc: new Date().toISOString()
            });

        }

    };




    return (
        <div className="checkout-page">

            <div className="checkout-card">


                <h1>
                    Checkout
                </h1>


                <p>
                    Trip Id:
                </p>


                <strong>
                    {tripId}
                </strong>



                <div className={`checkout-status ${status}`}>

                    {status === "Processing" &&
                        "Processing your checkout..."
                    }


                    {status === "Ready" &&
                        "Ready"
                    }


                    {
                        (status === "Completed" ||
                         status === "Failed") &&
                        
                        <>
                            {status === "Completed" && "✓ "}
                            {notification?.message}
                        </>
                    }

                </div>



                {status === "Ready" &&
                    (
                        <button
                            className="checkout-button"
                            onClick={handleCheckout}
                        >
                            Checkout
                        </button>
                    )
                }


            </div>

        </div>
    );
}