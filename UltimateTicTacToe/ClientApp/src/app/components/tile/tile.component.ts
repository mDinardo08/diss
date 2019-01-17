import { Component, Output, EventEmitter, Input, OnInit } from "@angular/core";
import { AbstractBoardGameComponent } from "../abstractBoardGame/abstractBoardGame.component";
import { Player } from "../../models/player/player.model";

@Component({
    selector: "tile",
    templateUrl: "./tile.component.html",
    styleUrls: ["./tile.component.styles.css"]
})

export class TileComponent extends AbstractBoardGameComponent {
    makeMove($event: any): any {
        this.moveEvent.emit();
    }

}
