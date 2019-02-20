import { GameSetupComponent } from "./setup.component";

describe("Game Setup", () => {

    let comp: GameSetupComponent;
    let mockUserService;
    beforeEach(() => {
        mockUserService = jasmine.createSpyObj("UserService", ["getUserId"]);
        comp = new GameSetupComponent(mockUserService);
    });

    it("Will create an empty list on init", () => {
        comp.ngOnInit();
        expect(comp.players).toBeDefined();
    });

    it("Will create a list of length 2 on init", () => {
        comp.ngOnInit();
        expect(comp.players.length).toBe(2);
    });

    it("Will assign type1 to the string passed in", () => {
        comp.ngOnInit();
        comp.player1Selected("some player");
        expect(comp.type1).toEqual("some player");
    });

    it("Will add a player to the first index of the array when player one is selected", () => {
        comp.ngOnInit();
        comp.player1Selected("");
        expect(comp.players[0]).toBeDefined();
    });

    it("Will assign the correct numerical type to player1", () => {
       comp.ngOnInit();
       comp.player1Selected("HUMAN");
       expect(comp.players[0].type).toBe(4);
    });

    it("Will assign colour 0 to player1", () => {
        comp.ngOnInit();
        comp.player1Selected("HUMAN");
        expect(comp.players[0].colour).toBe(0);
    });

    it("Will assign a name that is the type of the player", () => {
        comp.ngOnInit();
        comp.player1Selected("HUMAN");
        expect(comp.players[0].name).toBe("HUMAN");
    });

    it("Will assign the UserId as the numerical value of the player", () => {
        comp.ngOnInit();
        comp.player1Selected("RANDOM");
        expect(comp.players[0].userId).toBe(0);
    });

    it("Will assign the user id from the user service if player type is HUMAN", () => {
        mockUserService.getUserId.and.returnValue(1000);
        comp.ngOnInit();
        comp.player1Selected("HUMAN");
        expect(comp.players[0].userId).toBe(1000);
    });

    it("Will assign type2 to the string passed in", () => {
        comp.ngOnInit();
        comp.player2Selected("some player");
        expect(comp.type2).toEqual("some player");
    });

    it("Will add a player to the second index of the array when player one is selected", () => {
        comp.ngOnInit();
        comp.player2Selected("");
        expect(comp.players[1]).toBeDefined();
    });

    it("Will assign the correct numerical type to player2", () => {
       comp.ngOnInit();
       comp.player2Selected("HUMAN");
       expect(comp.players[1].type).toBe(4);
    });

    it("Will assign colour 1 to player2", () => {
        comp.ngOnInit();
        comp.player2Selected("HUMAN");
        expect(comp.players[1].colour).toBe(1);
    });

    it("Will assign a name that is the type of the player for player 2", () => {
        comp.ngOnInit();
        comp.player2Selected("HUMAN");
        expect(comp.players[1].name).toBe("HUMAN");
    });

    it("Will assign the UserId as the numerical value of the player for player 2", () => {
        comp.ngOnInit();
        comp.player2Selected("RANDOM");
        expect(comp.players[1].userId).toBe(0);
    });

    it("Will assign the user id from the user service if player type is HUMAN for player 2", () => {
        mockUserService.getUserId.and.returnValue(1000);
        comp.ngOnInit();
        comp.player2Selected("HUMAN");
        expect(comp.players[1].userId).toBe(1000);
    });

    it("Will not emit an event if one player is undefined", () => {
        comp.ngOnInit();
        spyOn(comp.opponentSelectedEvent, "emit");
        comp.play();
        expect(comp.opponentSelectedEvent.emit).not.toHaveBeenCalled();
    });

    it("Will set the number of games to one by default", () => {
        comp.ngOnInit();
        expect(comp.getNoGames()).toBe(1);
    });

    it("Will return the number of games to be played", () => {
        comp.ngOnInit();
        comp.noGames = 10;
        expect(comp.getNoGames()).toBe(10);
    });
});
