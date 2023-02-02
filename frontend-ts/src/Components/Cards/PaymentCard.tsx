import React from "react";
import Payment from "../../Models/Payment";

type PaymentCardProps = {
    payment: Payment
}

export default function PaymentCard (props: PaymentCardProps){

    let color = (props.payment.amount > 0)? "green" : "red";

    return(
        <div className={`payment-card ${color}`}>
            <div className={"payment-name"}>
                {props.payment.paymentName}
            </div>
            <div className={`payment-amount ${color}`}>
                {(props.payment.amount / 100).toFixed(2)} PLN
            </div>
            <div className={"payment-category"}>
                {props.payment.category}
            </div>
            <div className={"payment-time"}>
                {new Date(props.payment.paymentTime).toLocaleDateString("pl")}
            </div>

        </div>
    )

}