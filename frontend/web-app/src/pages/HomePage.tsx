import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { getTrips, type Trip } from "../api/trips";
import "../styles/HomePage.css";

function getTripTypeLabel(tripType: number): string {
    return tripType === 1 ? "Round trip" : "One way";
}

function getFlightDirectionLabel(direction: number): string {
    return direction === 1 ? "Return" : "Outbound";
}

function formatDate(value: string): string {
    return new Date(value).toLocaleString();
}

export function HomePage() {
    const navigate = useNavigate();
    const [trips, setTrips] = useState<Trip[]>([]);
    const [isLoading, setIsLoading] = useState(true);
    const [error, setError] = useState<string | null>(null);

    useEffect(() => {
        void getTrips()
            .then(setTrips)
            .catch(() => setError("Trips could not be loaded."))
            .finally(() => setIsLoading(false));
    }, []);

    return (
        <div className="home-page">
            <div className="home-container">
                <h1 className="home-title">My Reservations</h1>

                <div className="reservation-list">
                    {isLoading && <p>Loading trips...</p>}
                    {error && <p>{error}</p>}

                    {trips.map(trip => (
                        <div className="reservation-card" key={trip.id}>
                            <div className="reservation-header">
                                <h2>Trip #{trip.tripNo}</h2>
                            </div>

                            {trip.tripItems.map(reservation => (
                                <div className="trip-item" key={reservation.id}>
                                    <div className="trip-item-header">
                                        <strong>Flight reservation #{reservation.id}</strong>
                                        <span className="reservation-status">
                                            {getTripTypeLabel(reservation.tripType)}
                                        </span>
                                    </div>

                                    {reservation.flights.map((flight, flightIndex) => (
                                        <div className="details" key={`${reservation.id}-${flightIndex}`}>
                                            <strong>{getFlightDirectionLabel(flight.direction)}</strong>

                                            {flight.segments.map(segment => (
                                                <div className="segment" key={segment.id}>
                                                    <div className="route">
                                                        <strong>{segment.from}</strong>
                                                        <span>→</span>
                                                        <strong>{segment.to}</strong>
                                                    </div>
                                                    <span className="segment-times">
                                                        {formatDate(segment.departure)} – {formatDate(segment.arrival)}
                                                    </span>
                                                </div>
                                            ))}
                                        </div>
                                    ))}
                                </div>
                            ))}

                            <button
                                className="checkout-button"
                                onClick={() => navigate(`/checkout/${trip.id}`)}
                            >
                                Go To Checkout
                            </button>
                        </div>
                    ))}
                </div>
            </div>
        </div>
    );
}
