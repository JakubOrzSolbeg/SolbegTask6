import React from "react";
import AccountDetails from "../../Models/AccountDetails";
import Loader from "../Shared/Loader";
import MakeApiCall from "../../services/ApiService";
import {FontAwesomeIcon} from "@fortawesome/react-fontawesome";
import {faCirclePlus} from "@fortawesome/free-solid-svg-icons";

type AccountCartState = {
    isLoaded: boolean,
    isSuccess: boolean,
    accountData: AccountDetails | undefined
}


export class AccountCart extends React.Component<any, AccountCartState>{

    state: AccountCartState = {
        isLoaded: false,
        isSuccess: false,
        accountData: undefined
    }

    componentDidMount() {
        this.loadData();
    }

    loadData(){
        MakeApiCall<null, AccountDetails>("Accounts/AccountDetails", "GET", null)
            .then(res => {
                if (res.isSuccess){
                    this.setState({isLoaded: true, isSuccess: true, accountData: res.body})
                }
                else{
                    this.setState({isLoaded: true, isSuccess: false})
                }
            })
    }

    render() {

        if (!this.state.isLoaded){
            return (
                <div className={"account-cart"}>
                    <Loader />
                </div>)
        }
        else if (!this.state.isSuccess){
            return (
                <div className={"account-cart"}>
                    <p> Could not load data...</p>
                </div>)
        }
        else{
            let symbol;
            let color;
            if (this.state.accountData!.todayAccountChange > 0){
                symbol = "+";
                color = "green";
            }
            else if(this.state.accountData!.todayAccountChange < 0){
                symbol = "-";
                color = "red";
            }
            else{
                symbol = "";
                color = "grey";
            }

            let spending_limit = "no limit";
            if (this.state.accountData!.dailySpendingLimit !== null){
                spending_limit = (this.state.accountData!.dailySpendingLimit / 100).toFixed(2) + " PLN"
            }


            return (
                <div className={"account-cart"}>
                    <div className={"welcome-text"}>
                        <h1> Welcome back <b> {this.state.accountData?.login} </b> </h1> </div>
                    <div className={"balance"} >
                        {(this.state.accountData!.balance / 100).toFixed(2)} PLN
                    </div>
                    <div className={"account-change "+ color} >
                        {symbol} {(this.state.accountData!.todayAccountChange / 100).toFixed(2)} PLN
                    </div>
                    <div className={"spending-limit"}>
                        Spending limit: {spending_limit}
                    </div>
                    <div className={"account-buttons"}>
                        <a id={"new-payment-button"} href={"/new"} > <FontAwesomeIcon icon={faCirclePlus} /> </a>
                    </div>
                </div>
            )
        }
    }

}