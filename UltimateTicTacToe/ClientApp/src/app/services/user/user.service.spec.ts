import { UserService } from "./user.service";
import { Observable } from "rxjs/Observable";
import { RatingDTO } from "../../models/DTOs/RatingDTO";

describe("User service", () => {

    let service: UserService;

    beforeEach(() => {
        service = new UserService(null, null);
    });

    it("Will call the api to post to the correct end point to create user", () => {
        const api = jasmine.createSpyObj("ApiService", ["post"]);
        api.post.and.returnValue(Observable.of());
        service = new UserService(api, null);
        service.createUser();
        expect(api.post).toHaveBeenCalledWith("User/createUser", null);
    });

    it("Will emit a userUpdatedEvent when the api returns a RatingDTO", () => {
        const dto = new RatingDTO();
        const obsv = Observable.of(dto);
        const api = jasmine.createSpyObj("ApiService", ["post"]);
        api.post.and.returnValue(obsv);
        service = new UserService(api, jasmine.createSpyObj("ToasterService", ["pop"]));
        spyOn(service.userUpdatedEvent, "emit");
        service.createUser();
        expect(service.userUpdatedEvent.emit).toHaveBeenCalledWith(dto);
    });

    it("Will call to post the user id to the correct endpoint", () => {
        const api = jasmine.createSpyObj("ApiService", ["post"]);
        api.post.and.returnValue(Observable.of());
        service = new UserService(api, null);
        service.login(4);
        expect(api.post).toHaveBeenCalledWith("User/login", 4);
    });

    it("Will emit the rating dto returned by the api", () => {
        const dto = new RatingDTO();
        const obsv = Observable.of(dto);
        const api = jasmine.createSpyObj("ApiService", ["post"]);
        api.post.and.returnValue(obsv);
        service = new UserService(api, jasmine.createSpyObj("ToasterService", ["pop"]));
        spyOn(service.userUpdatedEvent, "emit");
        service.login(0);
        expect(service.userUpdatedEvent.emit).toHaveBeenCalledWith(dto);
    });
});
