import { Component, OnInit, ViewChild, ViewContainerRef } from "@angular/core";
import { GameService } from "../../services";
import { BoardGame } from "../../models/boardGame/boardgame/boardgame.model";
import { BoardCreationDTO } from "../../models/DTOs/BoardCreationDTO";
import { Player } from "../../models/player/player.model";
import { PlayerType } from "../../models/player/player.type.enum";

@Component({
    selector: "game",
    templateUrl: "./game.component.html"
})

export class GameComponent  {

    constructor(private gameService: GameService) {}

    public board: BoardGame;


    public moveMade($event): void {
        this.gameService.makeMove(this.board.board);
    }
}
