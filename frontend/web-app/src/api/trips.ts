export interface FlightSegment {
    id: number;
    previousSegmentId: number | null;
    from: string;
    to: string;
    departure: string;
    arrival: string;
}

export interface Flight {
    direction: number;
    segments: FlightSegment[];
}

export interface FlightReservation {
    id: number;
    tripType: number;
    flights: Flight[];
}

export interface Trip {
    id: string;
    tripNo: number;
    tripItems: FlightReservation[];
}

interface GetTripsResponse {
    trips: Trip[];
}

export async function getTrips(): Promise<Trip[]> {
    const response = await fetch("/v1/trips", {
        credentials: "include"
    });

    if (!response.ok) {
        throw new Error(
            `Trips could not be retrieved. Status: ${response.status}`
        );
    }

    const body: GetTripsResponse = await response.json();

    return body.trips;
}
