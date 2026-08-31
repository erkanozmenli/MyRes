import { useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { useAuth } from "../auth/AuthProvider";
import "../styles/LoginPage.css";
import { redirectToLogin } from "../auth/navigation";

export function LoginPage() {

    const navigate = useNavigate();

    const {
        user,
        loading
    } = useAuth();


    useEffect(() => {

        if (loading) {
            return;
        }

        if (user?.isAuthenticated) {
            navigate("/", { replace: true });
        }

    }, [loading, user, navigate]);


    const handleSignIn = () => {

        redirectToLogin("/");

    };


    if (loading) {
        return null;
    }


    if (user?.isAuthenticated) {
        return null;
    }


    return (
        <div className="login-page">
            <div className="login-card">
                <h1 className="login-logo">MyRes</h1>

                <h2>Welcome Back</h2>

                <p>Continue to your account</p>

                <button
                    className="login-button"
                    type="button"
                    onClick={handleSignIn}
                >
                    Sign In
                </button>
            </div>
        </div>
    );
}