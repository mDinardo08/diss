import { Component } from "@angular/core";
import { UserService } from "../../services/user/user.service";

@Component({
    templateUrl: "./user.component.html",
    selector: "user"
})
export class UserComponent {

    constructor(private userService: UserService) {}
}
