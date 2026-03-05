Namespace View.Recorte

    Public Module Enumeraciones
        
        Public Enum DrawStates As Integer
            WaitState = 0
            PreDrawComponent = 1
            DrawComponent = 2
            ComponentSelected = 3
            MovingUpLeft = 4
            MovingUp = 5
            MovingUpRight = 6
            MovingLeft = 7
            MovingRight = 8
            MovingDownLeft = 9
            MovingDown = 10
            MovingDownRight = 11
            MovingAll = 12
        End Enum

        Public Enum Movables As Integer
            NoMovable = 0
            MovableAll = 1
            movableUpLeft = 2
            movableUP = 3
            movableUpRight = 4
            movableLeft = 5
            movableRight = 6
            movableDownLeft = 7
            movableDown = 8
            movableDownRight = 9
        End Enum

    End Module

End Namespace