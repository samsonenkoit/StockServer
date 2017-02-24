import { Component, OnInit } from "@angular/core";

import { UserService } from "../service/user.service";
import { UserInfoAggregate } from "../model/userInfoAggregate.model";

@Component({
    selector: "user-info",
    templateUrl: "./app/view/userInfo.template.html"
})

export class UserInfoComponent implements OnInit {

    userInfoAggregate: UserInfoAggregate

    constructor(private userService: UserService) {

    }

    ngOnInit() {

        this.userService.getInfo().subscribe(response => {
            this.userInfoAggregate = <UserInfoAggregate>response;
            console.log(this.userInfoAggregate);
        });

    }
}