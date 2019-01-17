import { Component, Output, EventEmitter, Input, OnInit } from "@angular/core";
import { AbstractBoardGameComponent } from "../abstractBoardGame/abstractBoardGame.component";
import { Player } from "../../models/player/player.model";

@Component({
    selector: "tile",
    templateUrl: "./tile.component.html"
})

export class TileComponent extends AbstractBoardGameComponent {
    @Output() moveEvent =  new EventEmitter();

    makeMove(): any {
        this.moveEvent.emit();
    }

}
