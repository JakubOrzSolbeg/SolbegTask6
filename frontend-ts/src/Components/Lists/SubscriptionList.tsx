import React from "react";
import Subscription from "../../Models/Subscription";
import SubscriptionCard from "../Cards/SubscriptionCard";
import MakeApiCall from "../../services/ApiService";

type SubscriptionListProps = {
    subscriptions: Array<Subscription>,
    color: string,
    name: string
}

export default function (props: SubscriptionListProps){


    return(
        <div className={"subscription-list "+props.color}>
            <h2> {props.name} </h2>
            {props.subscriptions.map((element) => {
                return (<SubscriptionCard key={element.subscriptionId} color={props.color} subscription={element} />)
            })}

        </div>
    )
}