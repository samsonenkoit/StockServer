import { UserInfo } from "../model/userInfo.model";
import { Purchase } from "../model/purchaseInfo.model";

export class UserInfoAggregate {
    constructor(
        public user: UserInfo,
        public purchase: Purchase[]
    ) { }
}