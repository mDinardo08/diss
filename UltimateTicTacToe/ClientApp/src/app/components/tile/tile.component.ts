import { Component, Output, EventEmitter } from "@angular/core";
import { BoardGameComponent } from "../boardGame/boardGame.interface";
import { BoardGame } from "../../models/boardGame/boardgame/boardgame.model";

@Component({
    selector: "tile",
    templateUrl: "./tile.component.html"
})

export class TileComponent implements BoardGameComponent {
    @Output() moveEvent =  new EventEmitter();

    makeMove(): any {
        this.moveEvent.emit();
    }

    setBoard(board: BoardGame): void {
        throw new Error("Method not implemented.");
    }
}
