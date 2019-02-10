import { ApiService } from "../api/api.service";
import { Output, EventEmitter, Injectable } from "@angular/core";
import { RatingDTO } from "../../models/DTOs/RatingDTO";
import { ToastrService } from "ngx-toastr";

@Injectable()
export class UserService {
    @Output() userUpdatedEvent = new EventEmitter<RatingDTO>();
    private User: RatingDTO;
    constructor(private api: ApiService, private toastr: ToastrService) {
    }

    createUser(): void {
        this.api.post("User/createUser", null).subscribe((res: RatingDTO) => {
            this.toastr.success("Logged in as " + res.UserId);
            this.User = res;
            this.userUpdatedEvent.emit(res);
        }, (err) => {
            this.toastr.error("Something went wrong");
        });
    }

    login(userId: number): void {
        this.api.post("User/login", userId).subscribe((res: RatingDTO) => {
            this.toastr.success("Logged in as " + res.UserId);
            this.User = res;
            this.userUpdatedEvent.emit(res);
        }, (err) => {
            this.toastr.error("Something went wrong");
        });
    }

    getUser(): RatingDTO {
        return this.User;
    }


}
