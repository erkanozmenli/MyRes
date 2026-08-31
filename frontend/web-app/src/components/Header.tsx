import { useAuth } from "../auth/AuthProvider";
import { redirectToLogin } from "../auth/navigation";
import "./Header.css";

export function Header() {
    const { user, loading} = useAuth();

    const isAuthenticated = user?.isAuthenticated === true;

    const handleLogin = () => {
        redirectToLogin("/");
    };

    

    return (
        <header className="app-header">
            <div className="app-header-inner">

                <a href="/" className="app-header-logo">
                    MyRes
                </a>

                <div className="app-header-actions">

                    {!loading && isAuthenticated && (
                        <>
                            {user?.username && (
                                <span className="app-header-user">
                                    {user.username}
                                </span>
                            )}

                            <a
                                href="/bff/logout"
                                className="app-header-button"
                            >
                                Logout
                            </a>

                        </>
                    )}

                    {!loading && !isAuthenticated && (
                        <button
                            type="button"
                            className="app-header-button"
                            onClick={handleLogin}
                        >
                            Login
                        </button>
                    )}

                </div>

            </div>
        </header>
    );
}