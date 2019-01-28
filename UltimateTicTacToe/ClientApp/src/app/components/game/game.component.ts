import { Component, OnInit } from "@angular/core";
import { BoardGame } from "../../models/boardGame/boardgame/boardgame.model";
import { Move } from "../../models/move/move.model";
import { AbstractGameService } from "../../services/game/game.service.abstract";
import { Player } from "../../models/player/player.model";
import { PlayerType } from "../../models/player/player.type.enum";
import { PlayerColour } from "../../models/player/player.colour.enum";

@Component({
    selector: "game",
    templateUrl: "./game.component.html",
    styleUrls: ["./game.component.styles.css"]
})

export class GameComponent implements OnInit {

    constructor(private gameService: AbstractGameService) {}

    public game: BoardGame;
    public availableMoves: Array<Move>;
    public lastMove: Move;
    public overlayVisable: boolean;

    public ngOnInit(): void {
        this.game = new BoardGame();
        const human = new Player();
        human.type = PlayerType.HUMAN;
        human.name = "human";
        human.colour = PlayerColour.BLUE;
        const ai  = new Player();
        ai.colour = PlayerColour.RED;
        ai.name = "ai";
        ai.type = PlayerType.HUMAN;
        this.overlayVisable = true;
        this.gameService.createGame(3, [human, ai]);
        this.gameService.boardUpdatedEvent.subscribe((res: Array<Array<BoardGame>>) => {
            this.boardUpdated();
        });
    }

    public moveMade($event: Move): void {
        this.overlayVisable = true;
        this.gameService.makeMove($event);
    }

    private boardUpdated(): void {
        this.game.board = this.gameService.getBoard();
        this.availableMoves = this.gameService.getAvailableMoves();
        this.lastMove = this.gameService.getLastMove();
        this.overlayVisable = false;
    }
}
