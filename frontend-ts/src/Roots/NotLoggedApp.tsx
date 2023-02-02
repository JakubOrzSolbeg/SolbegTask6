import React from "react";
import {BrowserRouter as Router, Navigate, Route, Routes} from "react-router-dom";
import WelcomePanel from "../Components/Panels/WelcomePanel";
import LoginForm from "../Components/Forms/LoginForm";
import RegisterForm from "../Components/Forms/RegisterForm";

export default function NotLoggedApp(){

    return(
        <section className={"landing_page"}>
                <Router>
                    <Routes>
                        <Route path={"/"} element={
                            <WelcomePanel />
                            }
                        />
                        <Route path={"/login"} element={
                            <LoginForm />
                            }
                        />
                        <Route path={"/register"} element={
                            <RegisterForm />
                        }
                        />
                        <Route path='*' element={
                            <Navigate to="/" replace={true} />
                        }
                        />
                    </Routes>
                </Router>
        </section>
    )
}