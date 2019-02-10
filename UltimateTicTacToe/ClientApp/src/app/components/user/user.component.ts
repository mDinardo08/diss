import { Component, OnInit } from "@angular/core";
import { UserService } from "../../services/user/user.service";
import { Router } from "@angular/router";
import { ToastrService } from "ngx-toastr";

@Component({
    templateUrl: "./user.component.html",
    selector: "user"
})

export class UserComponent implements OnInit {

    UserId: number;

    constructor(private userService: UserService, private router: Router, private toast: ToastrService) {}

    ngOnInit() {
        this.userService.userUpdatedEvent.subscribe((res) => {
            this.router.navigate(["game"]);
        });
    }

    public login() {
        if (this.UserId < 5) {
            this.toast.error("Invalid Id");
        } else {
            this.userService.login(this.UserId);
        }
    }

    public createUser() {
        this.userService.createUser();
    }
}
