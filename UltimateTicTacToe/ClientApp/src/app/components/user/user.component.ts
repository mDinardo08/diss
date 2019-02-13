import { Component, OnInit } from "@angular/core";
import { UserService } from "../../services/user/user.service";
import { Router } from "@angular/router";
import { ToasterService } from "angular2-toaster";

@Component({
    templateUrl: "./user.component.html",
    selector: "user"
})

export class UserComponent implements OnInit {

    UserId: number;
    overlayVisable = false;
    constructor(private userService: UserService, private router: Router, private toast: ToasterService) {}

    ngOnInit() {
        this.userService.userUpdatedEvent.subscribe((res) => {
            this.overlayVisable = false;
            this.router.navigate(["game"]);
        });
    }

    public login() {
        if (this.UserId < 3) {
            this.toast.pop("error", "Invalid Id");
        } else {
            this.userService.login(this.UserId);
            this.overlayVisable = true;
        }
    }

    public createUser() {
        this.userService.createUser();
    }
}
