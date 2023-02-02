import React from "react";
import MakeApiCall from "../../services/ApiService";
import TransferCategories from "../../Models/IncomeCategories";

type SubscriptionTypeDict = {[key: number] : string}

type TransferFormState = {
    isIncome: boolean,
    name: string,
    amount: string,
    categoryName: string,
    incomeCategories: Array<string>,
    outcomeCategories: Array<string>,
    isCustomCategory: boolean,
    subscriptionTypes: SubscriptionTypeDict,
    selectedSubscriptionType: number,
    comment: string,
    errors: string
}

type AddSubscriptionRequest = {
    type: number,
    subscriptionName: string,
    categoryName: string,
    amount: number
    isIncome: boolean
    comment: string | null
}


export class TransferForm extends React.Component<any, TransferFormState>{

    constructor(props: any) {
        super(props);
        this.handleChange = this.handleChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);

    }

    state: TransferFormState = {
        isIncome: true,
        name: "",
        amount: "",
        incomeCategories: [],
        outcomeCategories: [],
        isCustomCategory: false,
        categoryName: "",
        subscriptionTypes: {},
        selectedSubscriptionType: 7,
        comment: "",
        errors: ""
    }

    componentDidMount() {
        MakeApiCall<null, TransferCategories>("Payments/PaymentCategories", "GET", null)
            .then(res => {
                if (res.isSuccess){
                    this.setState({
                        incomeCategories: res.body?.incomeCategories ?? [],
                        outcomeCategories : res.body?.outcomeCategories ?? []})
                }

            });
        MakeApiCall<null, SubscriptionTypeDict>("Payments/SubscriptionTypes", "GET", null)
            .then(res => {
                if (res.isSuccess){
                    this.setState({subscriptionTypes: res.body!})
                }
            });
    }

    handleChange(evt: React.ChangeEvent<HTMLInputElement>) {
        console.log(`Name ${evt.target.name} value ${evt.target.value}`)
        switch (evt.target.name) {
            case "minPrice":
                console.log("Min price");
                break;
            case "paymentType":
                this.setState({isIncome: evt.target.value === "income"});
                break;
            case "transferCategory":
                if (evt.target.value === "Other"){
                    this.setState({isCustomCategory: true, categoryName: "Custom"})
                }
                else{
                    this.setState({isCustomCategory: false, categoryName: evt.target.value})
                }
                break;
            case "customCategory":
                this.setState({categoryName: evt.target.value})
                break;
            case "subscriptionType":
                this.setState({selectedSubscriptionType: parseInt(evt.target.value)});
                break;
            case "comment":
                this.setState({comment: evt.target.value});
                break;
            case "subName":
                this.setState({name: evt.target.value});
                break;
            case "amount":
                this.setState({amount: evt.target.value});
                break;
            default:
                break;
        }
    }

    handleSubmit(evt: React.FormEvent<HTMLFormElement>){
        evt.preventDefault();

        let parsedAmount = parseFloat(this.state.amount.replace(',', '.'));

        let requestData: AddSubscriptionRequest = {
            type: this.state.selectedSubscriptionType,
            isIncome: this.state.isIncome,
            amount: parsedAmount * 100,
            subscriptionName: this.state.name,
            categoryName: this.state.categoryName,
            comment: this.state.comment
        }

       MakeApiCall<AddSubscriptionRequest, boolean>("Payments/AddSubscription", "POST", requestData)
           .then(res => {
               if (res.isSuccess){
                   window.location.replace("/");
               }
               else{
                   this.setState({errors: res.errors})
               }
           })
    }

    render() {
        let transferCategory = (this.state.isIncome? this.state.incomeCategories : this.state.outcomeCategories)
            .map(category => {
                return(
                    <label key={category}>
                        <input key={category} onChange={this.handleChange} type="radio"
                               value={category} name="transferCategory" title={category} />
                        {category}
                    </label>
                        )
            })

        return (
            <div className={"transfer-panel"}>
                <h2> Prepare transfer </h2>
                <form onSubmit={this.handleSubmit}>
                    <div>
                        <input checked={this.state.isIncome} onChange={this.handleChange} type="radio" value="income" name="paymentType" /> Income
                        <input checked={!this.state.isIncome} onChange={this.handleChange} type="radio" value="outcome" name="paymentType" /> Expense
                    </div>
                    <label>
                    Name:
                    <input required={true} type={"text"} name={"subName"} value={this.state.name}
                           onChange={this.handleChange} placeholder={"Transfer name"}/>
                    </label>
                    <label>
                    Amount:
                    <input onChange={this.handleChange} required={true} type={"text"} name={"amount"} value={this.state.amount}
                           placeholder={"Transfer amount"}/>
                    </label>

                        {transferCategory}
                    <div>
                        <input onChange={this.handleChange} type="radio" value="Other" name="transferCategory"  />
                        <input disabled={!this.state.isCustomCategory} type={"text"}
                           placeholder={"Custom category"} name={"customCategory"}
                           value={(this.state.isCustomCategory? this.state.categoryName : "")}
                            onChange={this.handleChange}
                        />
                    </div>
                    <div>
                        {Object.entries(this.state.subscriptionTypes).map(([k, v]) => {

                            return(
                                <label key={k}>
                                    <input checked={this.state.selectedSubscriptionType === parseInt(k)}
                                           type={"radio"} name={"subscriptionType"} value={k} onChange={this.handleChange}/>
                                    {v}
                                </label>);
                        })}
                    </div>
                    <label>
                        Comment:
                        <input type={"text"} name={"comment"} onChange={this.handleChange}
                               value={this.state.comment} placeholder={"Addictional comments"} />
                    </label>
                    <span id={"form-errors"}>{this.state.errors}</span>
                    <input type={"submit"} value={"Send"} />
                </form>

            </div>
        )
    }
}
