import { Component, OnInit, ViewChild, ViewContainerRef, ComponentRef } from "@angular/core";
import { BoardGameComponent } from "../boardGame/boardGame.interface";
import { BoardGame } from "../../models/boardGame/boardgame/boardgame.model";
import { BoardGameFactory } from "../../logicComponents/boardgameFactory/boardGame.factory";

@Component({
    selector: "ultimateTictactoe",
    templateUrl: "./ultimateTictactoe.component.html"
})

export class TictactoeComponent implements BoardGameComponent {

    boardRefs = new Array<Array<ComponentRef<BoardGameComponent>>>();
    @ViewChild("boardView", {read: ViewContainerRef}) boardView;

    constructor(private boardFactory: BoardGameFactory) {}

    setBoard(board: BoardGame): void {
        this.boardView.clear();
        const factory = this.boardFactory.createBoardgame(board);
        const boardRef = this.boardView.createComponent(factory);
        this.boardRefs.push(new Array<ComponentRef<BoardGameComponent>>(boardRef));
    }

}
