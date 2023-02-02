export default interface Subscription {
    subscriptionId: number
    subscriptionName: string
    categoryName: string
    comment: string
    subscriptionType: string
    amount: number
    startTime: Date
    endTime: Date
}