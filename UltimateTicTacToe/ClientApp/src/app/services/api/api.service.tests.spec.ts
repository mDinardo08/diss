import { ApiService } from "./api.service";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs/Observable";
describe("Api Service", () => {

    let service: ApiService;

    beforeEach(() => {
        service = new ApiService(null);
    });

    it("Will return the Url value from the config", () => {
        const url = service.getURL();
        expect(url).toBe("./api/");
    });

    it("Will call get on the Http client", () => {
        const mockHttp = jasmine.createSpyObj("HttpClient", ["get"]);
        service = new ApiService(mockHttp);
        spyOn(service, "getURL").and.returnValue("mockUrl");
        service.get("/some endpoint");
        expect(mockHttp.get).toHaveBeenCalledWith("mockUrl/some endpoint");
    });

    it("Will return the object returned from the http client", () => {
        const mockHttp = jasmine.createSpyObj("HttpClient", ["get"]);
        const observable = new Observable();
        mockHttp.get.and.returnValue(observable);
        service = new ApiService(mockHttp);
        const result = service.get<string>("end");
        expect(result).toBe(observable);
    });
});
