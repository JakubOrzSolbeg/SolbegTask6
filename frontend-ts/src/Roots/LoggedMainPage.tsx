import React from "react";
import Navigation from "../Components/Shared/Navigation";
import Footer from "../Components/Shared/Foot";
import {BrowserRouter as Router, Route, Routes} from "react-router-dom";
import WelcomePanel from "../Components/Panels/WelcomePanel";
import {AccountCart} from "../Components/Cards/AccountCart";
import {SubscriptionPanel} from "../Components/Panels/Subscriptions";
import {SettingsForm} from "../Components/Forms/SettingsForm";
import {TransferForm} from "../Components/Forms/TransferForm";
import {PaymentList} from "../Components/Lists/PaymentList";


export class LoggedMainPage extends React.Component<any, any>{

    render() {
        return (
            <main>
                <Router>
                <Navigation />
                    <Routes>
                        <Route path={"/"} element={
                            <div className={"content"}>
                                <AccountCart />
                                <SubscriptionPanel />
                                <PaymentList />
                            </div>
                        }
                        />
                        <Route path={"/settings"} element={
                            <div className={"content"}>
                                <SettingsForm />
                            </div>
                        }
                        />
                        <Route path={"/new"} element={
                            <div className={"content"}>
                                <TransferForm />
                            </div>
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