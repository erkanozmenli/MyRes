import * as signalR from "@microsoft/signalr";


export function createSignalRConnection() {
    return new signalR.HubConnectionBuilder()
        .withUrl(
            "/v1/notifications/hub",
            {
                withCredentials: true,
                skipNegotiation: true,
                transport: signalR.HttpTransportType.WebSockets
            }
        )
        .withAutomaticReconnect()
        .configureLogging(
            signalR.LogLevel.Information
        )
        .build();
}