export interface CurrentUser {
    isAuthenticated: boolean;
    userId?: string;
    username?: string;
    email?: string;
}

export async function getCurrentUser(): Promise<CurrentUser> {
    const response = await fetch("/bff/me", {
        credentials: "include"
    });

    if (!response.ok) {
        throw new Error("Failed to get current user");
    }

    return response.json();
}