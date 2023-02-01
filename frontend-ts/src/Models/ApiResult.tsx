export default interface ApiResult<T> {
    isSuccess: boolean,
    errors: string,
    body?: T
}