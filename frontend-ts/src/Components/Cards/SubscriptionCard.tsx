import React from "react";
import Subscription from "../../Models/Subscription";
import {FontAwesomeIcon} from "@fortawesome/react-fontawesome";
import {faTrashCan} from "@fortawesome/free-solid-svg-icons";
import MakeApiCall from "../../services/ApiService";

type SubscriptionCardProps = {
    color: string,
    subscription: Subscription
}

export default function SubscriptionCard (props: SubscriptionCardProps){

    const removeElement = (id: number): void => {
        MakeApiCall<null, boolean>(`Payments/CancelSubscription?subscriptionId=${id}`, "POST", null)
            .then(res => {
                if(res.isSuccess){

                }
                else{
                    alert(res.errors);
                }
            })
    }

    return(
        <div className={`subscription-card ${props.color}`}>
            <div className={"subscription-name"}>
                {props.subscription.subscriptionName}
            </div>
            {/*<div className={"subscription-type"}>*/}
            {/*    {props.subscription.subscriptionType}*/}
            {/*</div>*/}
            <div className={"subscription-amount"}>
                {(props.subscription.amount / 100).toFixed(2)} PLN
            </div>
            <div className={"subscription-time"}>
                {props.subscription.subscriptionType}  {new Date(props.subscription.startTime).toLocaleDateString("pl")}
            </div>
            <div className={"subscription-category"}>
                {props.subscription.categoryName}
            </div>
            <div className={"subscription-comments"}>
                {props.subscription.comment}
            </div>
            <div className={"action-buttons"}>
                <button className={"transparent-button red"} onClick={() => removeElement(props.subscription.subscriptionId)}>
                    <FontAwesomeIcon icon={faTrashCan} />
                </button>
            </div>

        </div>
    )
}