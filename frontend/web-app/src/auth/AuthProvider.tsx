import {
    createContext,
    useCallback,
    useContext,
    useEffect,
    useState
} from "react";

import { getCurrentUser } from "./auth";
import type { CurrentUser } from "./auth";

interface AuthContextValue {
    user: CurrentUser | null;
    loading: boolean;
    refresh: () => Promise<boolean>;
    logout: () => void;
}

const AuthContext = createContext<AuthContextValue | undefined>(
    undefined
);

interface AuthProviderProps {
    children: React.ReactNode;
}

export function AuthProvider({ children }: AuthProviderProps) {

    const [user, setUser] = useState<CurrentUser | null>(null);

    const [loading, setLoading] = useState(true);


    const refresh = useCallback(async (): Promise<boolean> => {

        try {

            const result = await getCurrentUser();

            setUser(result);

            return true;
        }
        catch (error) {

            console.error(
                "Authentication check failed",
                error
            );

            setUser(null);

            return false;
        }

    }, []);


    useEffect(() => {

        refresh()
            .finally(() => {

                setLoading(false);

            });

    }, [refresh]);


    useEffect(() => {

        const handleFocus = () => {

            refresh().catch(console.error);

        };


        window.addEventListener(
            "focus",
            handleFocus
        );


        return () => {

            window.removeEventListener(
                "focus",
                handleFocus
            );

        };

    }, [refresh]);


    const logout = () => {
        setUser(null);

        window.location.assign("/bff/logout");
    };


    return (

        <AuthContext.Provider
            value={{
                user,
                loading,
                refresh,
                logout
            }}
        >

            {children}

        </AuthContext.Provider>

    );
}


export function useAuth() {

    const context = useContext(AuthContext);

    if (!context) {

        throw new Error("useAuth must be used inside AuthProvider");

    }

    return context;
}