export async function startCheckout(tripId: string) {

    const response = await fetch(
        `/v1/trips/${tripId}/checkout`,
        {
            method: "POST",
            credentials: "include"
        }
    );


    if (response.status !== 202) {
        throw new Error(
            `Checkout failed. Status: ${response.status}`
        );
    }


    return response;
}