import React from "react";
import Payment from "../../Models/Payment";
import MakeApiCall from "../../services/ApiService";
import Loader from "../Shared/Loader";
import PaymentCard from "../Cards/PaymentCard";

type PaymentListState = {
    isLoaded: boolean,
    isSuccess: boolean,
    data: Array<Payment>,
    currentIndex: number
}

export class PaymentList extends React.Component<any, PaymentListState>{

    constructor(props: any) {
        super(props);

        this.loadData = this.loadData.bind(this);
    }

    state: PaymentListState = {
        isLoaded: false,
        isSuccess: true,
        data: [],
        currentIndex: 1
    }

    loadData(){
        let data_lenght = 1;
        MakeApiCall<null, Array<Payment>>(
            `Payments/Payments?start=${this.state.currentIndex}&end=${this.state.currentIndex + data_lenght}`,
            "GET", null)
            .then(res => {
                if (res.isSuccess){
                    this.setState({
                        data: [...this.state.data, ...res.body!],
                        currentIndex: this.state.currentIndex + (res.body?.length ?? 0),
                        isSuccess: true,
                        isLoaded: true
                    })
                }
                else{
                    this.setState({
                        isLoaded: true, isSuccess: false
                    })
                }
            });
        }

    componentDidMount() {
        this.loadData();
    }


    render() {
        if (!this.state.isLoaded){
            return (
                <div className={"payment-list"}>
                    <h3> Payments </h3>
                    <Loader />
                </div>)
        }
        else if (!this.state.isSuccess){
            return (
                <div className={"payment-list"}>
                    <p> Could not load data...</p>
                </div>)
        }
        else {
            return (
                <div className={"payment-list"}>
                    <h2> Transfers </h2>
                    {this.state.data.map(element => {
                        return(<PaymentCard key={element.paymentId} payment={element} />);
                    })}
                    <div className={"center-div"}>
                        <button className={"load-more-button"} onClick={() => this.loadData()}> Load more </button>
                    </div>

                </div>)

        }

    }


}