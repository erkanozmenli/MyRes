import { useEffect, useRef } from "react";
import { useAuth } from "./AuthProvider";
import { redirectToLogin } from "./navigation";

interface RequireAuthProps {
    children: React.ReactNode;
}

export function RequireAuth({
    children
}: RequireAuthProps) {

    const { user, loading } = useAuth();

    const loginStarted = useRef(false);

    useEffect(() => {

        if (loading) {
            return;
        }

        if (user?.isAuthenticated) {
            return;
        }

        if (loginStarted.current) {
            return;
        }

        loginStarted.current = true;

        redirectToLogin();

    }, [loading, user]);

    if (loading) {
        return <h1>Loading...</h1>;
    }

    if (!user?.isAuthenticated) {
        return null;
    }

    return <>{children}</>;
}