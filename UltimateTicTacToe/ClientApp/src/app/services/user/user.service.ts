import { ApiService } from "../api/api.service";
import { Output, EventEmitter } from "@angular/core";
import { RatingDTO } from "../../models/DTOs/RatingDTO";

export class UserService {
    @Output() userUpdatedEvent = new EventEmitter<RatingDTO>();

    constructor(private api: ApiService) {
    }

    createUser(): void {
        this.api.post("User/createUser", null).subscribe((res: RatingDTO) => {
            this.userUpdatedEvent.emit(res);
        });
    }

    login(userId: number): void {
        this.api.post("User/login", userId).subscribe((res: RatingDTO) => {
            this.userUpdatedEvent.emit(res);
        });
    }


}
