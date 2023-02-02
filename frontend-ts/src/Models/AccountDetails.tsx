import {strict} from "assert";

export default interface AccountDetails{
    login: string
    registerTime: Date,
    permissions: string,
    balance: number,
    todayAccountChange: number,
    dailySpendingLimit: number | null
}