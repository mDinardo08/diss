import { Injectable, ComponentFactoryResolver, Injector, ComponentFactory } from "@angular/core";
import { TileComponent } from "../../components/tile/tile.component";
import { BoardGameComponent } from "../../components/boardGame/boardGame.interface";
import { BoardGame } from "../../models/boardGame/boardgame/boardgame.model";
import { TictactoeComponent } from "../../components/ultimateTictactoe/ultimateTictactoe.component";

@Injectable()
export class BoardGameFactory {

    constructor(private componentFactoryResolver: ComponentFactoryResolver, private injector: Injector) {}

    public createBoardgame(boardGame: BoardGame): BoardGameComponent {
        const type = boardGame.board == null ? TileComponent : TictactoeComponent;
        const factory = this.componentFactoryResolver.resolveComponentFactory<BoardGameComponent>(type);
        const compRef = factory.create(this.injector);
        if (type === TictactoeComponent) {
            compRef.instance.setBoard(this.createBoardStructure(boardGame.board));
        }
        return null;
    }

    public createBoardStructure(board: Array<Array<BoardGame>>): Array<Array<BoardGameComponent>> {
        const result = new Array<Array<BoardGameComponent>>();
        for (let x = 0; x < board.length; x++) {
            result.push(new Array<BoardGameComponent>());
            for (let y = 0; y < board[x].length; y++) {
                result[x].push(this.createBoardgame(board[x][y]));
            }
        }
        return result;
    }
}
