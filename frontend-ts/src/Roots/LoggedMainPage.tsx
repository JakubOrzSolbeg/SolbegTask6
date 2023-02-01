import React from "react";
import Navigation from "../Components/Shared/Navigation";
import Footer from "../Components/Shared/Foot";
import {BrowserRouter as Router, Route, Routes} from "react-router-dom";
import WelcomePanel from "../Components/Panels/WelcomePanel";


export class LoggedMainPage extends React.Component<any, any>{

    render() {
        return (
            <main>
                <Router>
                <Navigation />
                    <Routes>
                        <Route path={"/"} element={
                            <p> Here will content be served </p>
                        }
                        />
                        <Route path='*' element={
                            <p> Element not found </p>
                        }
                        />

                    </Routes>
                </Router>
                <Footer />
            </main>

        )
    }
}