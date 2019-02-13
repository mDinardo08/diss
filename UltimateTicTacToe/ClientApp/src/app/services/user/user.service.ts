import { ApiService } from "../api/api.service";
import { Output, EventEmitter, Injectable } from "@angular/core";
import { RatingDTO } from "../../models/DTOs/RatingDTO";
import { ToasterService } from "angular2-toaster";

@Injectable()
export class UserService {
    @Output() userUpdatedEvent = new EventEmitter<RatingDTO>();
    private User: RatingDTO;
    constructor(private api: ApiService, private toastr: ToasterService) {
    }

    createUser(): void {
        this.api.post("User/createUser", null).subscribe((res: RatingDTO) => {
            this.toastr.pop("success", "Logged in as " + res.userId);
            this.User = res;
            this.userUpdatedEvent.emit(res);
        }, (err) => {
            this.toastr.pop("error", "Something went wrong");
        });
    }

    login(userId: number): void {
        this.api.post("User/login", userId).subscribe((res: RatingDTO) => {
            this.toastr.pop("success", "Logged in as " + res.userId);
            this.User = res;
            this.userUpdatedEvent.emit(res);
        }, (err) => {
            this.toastr.pop("error", "Something went wrong");
        });
    }

    getUserId(): number {
        return this.User === null || this.User === undefined ? -1 : this.User.userId;
    }


}
