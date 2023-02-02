import React from "react";
import AccountDetails from "../../Models/AccountDetails";
import Subscription from "../../Models/Subscription";
import MakeApiCall from "../../services/ApiService";
import SubscriptionOverview from "../../Models/SubscriptionOverview";
import Loader from "../Shared/Loader";
import SubscriptionList from "../Lists/SubscriptionList";

type SubscriptionsState = {
    isLoaded: boolean,
    isSuccess: boolean,
    data: SubscriptionOverview | undefined
}

export class SubscriptionPanel extends React.Component<any, SubscriptionsState>{

    state: SubscriptionsState = {
        isLoaded: false,
        isSuccess: true,
        data: undefined
    }


    loadData(){
        MakeApiCall<null, SubscriptionOverview>("Payments/Subscriptions", 'GET', null)
            .then(res => {
                if(res.isSuccess){
                    this.setState({isSuccess: true, isLoaded: true, data: res.body!})
                }
                else{
                    this.setState({isLoaded: true, isSuccess: false})
                }
            });
    }

    componentDidMount() {
        this.loadData();
    }

    render() {
        if (!this.state.isLoaded){
            return (
                <div className={"subscription-panel"}>
                    <Loader />
                </div>)
        }
        else if (!this.state.isSuccess){
            return (
                <div className={"subscription-panel"}>
                    <p> Could not load data...</p>
                </div>)
        }
        else {
            return(
                <div className={"subscription-panel"}>

                    <SubscriptionList name={"Incomes"} subscriptions={this.state.data!.incomes} color={"green"} key={"incomes"} />

                    <SubscriptionList name={"Expenses"} subscriptions={this.state.data!.outcomes} color={"red"} key={"outcomes"}/>
                </div>
            )
        }
    }

}
