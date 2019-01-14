import { Component, Output, EventEmitter } from "@angular/core";

@Component({
    selector: "tile",
    templateUrl: "./tile.component.html"
})

export class TileComponent {
    @Output() moveEvent =  new EventEmitter();

    makeMove(): any {
        this.moveEvent.emit();
    }


}
