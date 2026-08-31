import { Routes, Route, Navigate } from "react-router-dom";
import { RequireAuth } from "./auth/RequireAuth";

import { Header } from "./components/Header";

import { HomePage } from "./pages/HomePage";
import { LoginPage } from "./pages/LoginPage";
import { CheckoutPage } from "./pages/CheckoutPage";

function App() {
    return (
        <>
            <Header />

            <Routes>
                <Route
                    path="/"
                    element={<HomePage />}
                />

                <Route
                    path="/login"
                    element={<LoginPage />}
                />

                <Route
                    path="/checkout"
                    element={<Navigate to="/" />}
                />

                <Route
                    path="/checkout/:tripId"
                    element={
                        <RequireAuth>
                            <CheckoutPage />
                        </RequireAuth>
                    }
                />
            </Routes>
        </>
    );
}

export default App;