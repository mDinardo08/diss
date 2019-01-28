import { ApiService } from "./api.service";
import { HttpClient, HttpHeaders } from "@angular/common/http";
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
        const observable = new Observable<string>();
        mockHttp.get.and.returnValue(observable);
        service = new ApiService(mockHttp);
        const result = service.get<string>("end");
        expect(result).toBe(observable);
    });

    it("Will return an object with a httpOptions field", () => {
        const result = service.getOptions();
        expect(result.headers).toBeDefined();
    });

    it("Will have a httpHeaders object in the httpOptions field", () => {
        const result = service.getOptions();
        expect(result.headers instanceof HttpHeaders).toBe(true);
    });

    it("Will set content type to application json", () => {
        const result = service.getOptions();
        const options = <HttpHeaders>result.headers;
        expect(options.get("Content-Type")).toBe("application/json");
    });

    it("Will call to post at the correct URL", () => {
        const mockHttp = jasmine.createSpyObj("HttpClient", ["post"]);
        service = new ApiService(mockHttp);
        spyOn(service, "getURL").and.returnValue("url");
        spyOn(service, "getOptions").and.returnValue(null);
        service.post("/endpoint", null);
        expect(mockHttp.post).toHaveBeenCalledWith("url/endpoint", null, null);
    });

    it("Will call to post with the correct the http options", () => {
        const mockHttp = jasmine.createSpyObj("HttpClient", ["post"]);
        const options = {
            value:  {
                "stuff": "thigs"
            }
        };
        service = new ApiService(mockHttp);
        spyOn(service, "getURL").and.returnValue("");
        spyOn(service, "getOptions").and.returnValue(options);
        service.post("", null);
        expect(mockHttp.post).toHaveBeenCalledWith("", null, options);
    });

    it("Will call to post with correct data", () => {
        const mockHttp = jasmine.createSpyObj("HttpClient", ["post"]);
        service = new ApiService(mockHttp);
        spyOn(service, "getURL").and.returnValue("");
        spyOn(service, "getOptions").and.returnValue(null);
        const data = {
            value:  {
                "stuff": "thigs"
            }
        };
        service.post("", data);
        expect(mockHttp.post).toHaveBeenCalledWith("", data, null);
    });

    it("Will return whatever the HttpClient returns", () => {
        const mockHttp = jasmine.createSpyObj("HttpClient", ["post"]);
        const expected = new Observable<string>();
        mockHttp.post.and.returnValue(expected);
        service = new ApiService(mockHttp);
        spyOn(service, "getURL").and.returnValue("");
        spyOn(service, "getOptions").and.returnValue(null);
        const actual = service.post("", null);
        expect(actual).toBe(expected);
    });
});
