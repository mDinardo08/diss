import { Injectable, ComponentFactoryResolver, Injector, ComponentFactory } from "@angular/core";
import { TileComponent } from "../../components/tile/tile.component";
import { BoardGameComponent } from "../../components/boardGame/boardGame.interface";
import { BoardGame } from "../../models/boardGame/boardgame/boardgame.model";
import { TictactoeComponent } from "../../components/ultimateTictactoe/ultimateTictactoe.component";

@Injectable()
export class BoardGameFactory {

    constructor(private componentFactoryResolver: ComponentFactoryResolver) {}

    public createBoardgame(boardGame: BoardGame): ComponentFactory<BoardGameComponent> {
        const type = boardGame.board == null ? TileComponent : TictactoeComponent;
        return this.componentFactoryResolver.resolveComponentFactory<BoardGameComponent>(type);
    }
}
