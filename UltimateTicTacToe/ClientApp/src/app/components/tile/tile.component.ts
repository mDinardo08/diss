import { Component, Output, EventEmitter } from "@angular/core";
import { BoardGameComponent } from "../boardGame/boardGame.interface";
import { BoardGame } from "../../models/boardGame/boardgame/boardgame.model";
import { Player } from "../../models/player/player.model";
import { AbstractBoardGameComponent } from "../abstractBoardGame/abstractBoardGame.component";

@Component({
    selector: "tile",
    templateUrl: "./tile.component.html"
})

export class TileComponent extends AbstractBoardGameComponent {
    @Output() moveEvent =  new EventEmitter();

    makeMove(): any {
        this.moveEvent.emit();
    }

    setBoard(board: Array<Array<BoardGameComponent>>): void {
        throw new Error("Method not implemented.");
    }
}
