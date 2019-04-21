
namespace Hex.Engine.Lookahead
{
    using System;
    using System.Collections.Generic;
    using Hex.Board;
    using Hex.Engine.CandiateMoves;
    using Hex.Engine.PathLength;

    /// <summary>
    /// Minimax algorithm in the hex game
    /// this is look-ahead, the "brains" of the game,
    /// Move selection by exhaustive searching of look-ahead tree
    /// With alpha-beta pruning
    /// and killer heuristic
    /// which means simply that the first move that we consider is one that
    /// has previously produced an a-b cutoff at this ply
    /// which move to store? not the one at which you cut off, but the earlier one
    /// </summary>
    public class Minimax
    {
        //// input data, given in the constructor
        //private readonly HexBoard actualBoard;
        //private readonly GoodMoves goodMoves;
        //private readonly ICandidateMoves candidateMovesFinder;
        //private readonly BoardCache boardCache;
        //private readonly IPathLengthFactory pathLengthFactory;

        //// private readonly List<DebugDataItem> debugDataItems = new List<DebugDataItem>();

        //public Minimax(HexBoard board, GoodMoves goodMoves, ICandidateMoves candidateMovesFinder)
        //{
        //    this.actualBoard = board;
        //    this.goodMoves = goodMoves;
        //    this.candidateMovesFinder = candidateMovesFinder;

        //    this.boardCache = new BoardCache(board.Size);
        //    this.pathLengthFactory = new PathLengthAStarFactory();
        //}

        //public GoodMoves GoodMoves
        //{
        //    get { return this.goodMoves; }
        //}

        //public HexBoard ActualBoard
        //{
        //    get { return this.actualBoard; }
        //}

        //public TimeSpan MoveTime { get; private set; }

        //public int CountBoards { get; private set; }





        ///// <summary>
        ///// public wrapper for recursion
        ///// try each possible move, find the one with the best score
        ///// </summary>
        ///// <param name="lookahead">how far to look ahead</param>
        ///// <param name="playerX">is this for player x</param>
        ///// <returns>The data on the best move location and score</returns>
        //public MinimaxResult DoMinimax(int lookahead, bool playerX)
        //{
        //    // set up inital state
        //    DateTime startTime = DateTime.Now;

        //    if (lookahead < 1)
        //    {
        //        throw new Exception("Invalid lookahead of " + lookahead);
        //    }



        //    Occupied player = playerX.ToPlayer();
        //    int alpha = MoveScoreConverter.ConvertWin(player.Opponent(), 0);
        //    int beta = MoveScoreConverter.ConvertWin(player, 0);

        //    MinimaxResult bestMove = this.ScoreBoard(lookahead, this.ActualBoard, playerX, alpha, beta);

        //    if (bestMove.Move != Location.Null)
        //    {
        //        GoodMoves.AddGoodMove(0, bestMove.Move);
        //    }

        //    DateTime endTime = DateTime.Now;
        //    this.MoveTime = endTime - startTime;

        //    return bestMove;
        //}

        //private static bool IsAlphaBetaCutoff(bool isPlayerX, int alpha, int beta)
        //{
        //    if (isPlayerX)
        //    {
        //        return beta <= alpha;
        //    }

        //    return alpha <= beta;
        //}

        //private static int CheckAlpha(int alpha, int testAlpha, bool isPlayerX)
        //{
        //    if (isPlayerX && testAlpha > alpha)
        //    {
        //        return testAlpha;
        //    }

        //    if (!isPlayerX && testAlpha < alpha)
        //    {
        //        return testAlpha;
        //    }

        //    return alpha;
        //}

        ///// <summary>
        ///// private recursive worker - does the minimax algorithm
        ///// </summary>
        ///// <param name="lookahead">the current ply, counts down to zero</param>
        ///// <param name="stateBoard">the current board</param>
        ///// <param name="isPlayerX">player X or player Y</param>
        ///// <param name="alpha">alpha value used in alpha-beta pruning</param>
        ///// <param name="beta">beta value used in alpha-beta pruning</param>
        ///// <returns>the score of the board and best move location</returns>
        //private MinimaxResult ScoreBoard(int lookahead, HexBoard stateBoard, bool isPlayerX, int alpha, int beta)
        //{
        //    MinimaxResult moveScore;
        //    PathLengthBase staticAnalysis;
        //    if (lookahead==0)
        //    {
        //        staticAnalysis = this.pathLengthFactory.CreatePathLength(stateBoard);
        //        moveScore = new MinimaxResult(staticAnalysis.SituationScore());
        //    }
        //    this.CountBoards++;

        //    MinimaxResult bestResult = null;
        //    Location cutoffMove = Location.Null;

        //    var possibleMoves = this.candidateMovesFinder.CandidateMoves(stateBoard, lookahead);
        //    foreach (Location move in possibleMoves)
        //    {
        //        // end on null loc
        //        if (move.IsNull())
        //        {
        //            break;
        //        }


        //        // make a speculative board, like the current, but with this cell played
        //        HexBoard testBoard = this.boardCache.GetBoard();
        //        testBoard.CopyStateFrom(stateBoard);

        //        testBoard.PlayMove(move, isPlayerX);

        //        staticAnalysis = this.pathLengthFactory.CreatePathLength(testBoard);
        //        int situationScore = staticAnalysis.SituationScore();

        //        if (lookahead <= 1)
        //        {
        //            // we have reached the limits of lookahead - return the situation score
        //            moveScore = new MinimaxResult(situationScore);
        //        }
        //        else if (MoveScoreConverter.IsWin(situationScore))
        //        {
        //            // stop - someone has won
        //            moveScore = new MinimaxResult(situationScore);
        //        }
        //        else
        //        {
        //            // recurse 
        //            moveScore = this.ScoreBoard(lookahead - 1, testBoard, !isPlayerX, beta, alpha);
        //            moveScore.MoveWins();
        //        }

        //        moveScore.Move = move;

        //        this.boardCache.Release(testBoard);

        //        // higher scores are good for player x, lower scores for player y
        //        if (bestResult == null || MoveScoreConverter.IsBetterFor(moveScore.Score, bestResult.Score, isPlayerX))
        //        {
        //            bestResult = new MinimaxResult(move, moveScore);
        //        }

        //        // do the alpha-beta pruning 
        //        alpha = CheckAlpha(alpha, moveScore.Score, isPlayerX);

        //        if (IsAlphaBetaCutoff(isPlayerX, alpha, beta))
        //        {
        //            cutoffMove = move;
        //            bestResult.Score = alpha;
        //            break;
        //        }

        //        // end a-b pruning
        //    }

        //    if (bestResult != null)
        //    {
        //        GoodMoves.AddGoodMove(lookahead, bestResult.Move);
        //    }

        //    if (cutoffMove != Location.Null)
        //    {
        //        GoodMoves.AddGoodMove(lookahead, cutoffMove);
        //    }

        //    return bestResult;
        //}
        private HexBoard board;
        private bool maximizingPlayer;
        private int depth;
        private int alpha;
        private int beta;
        private readonly IPathLengthFactory pathLengthFactory;
        private readonly ICandidateMoves candidateMovesFinder;
        public GoodMoves GoodMoves
        {
            get;
            set;
        }
        public Minimax(HexBoard board, GoodMoves goodMoves, ICandidateMoves candidateMovesFinder)
        {
            this.board = board;
      
            this.depth = 0;

            this.candidateMovesFinder = candidateMovesFinder;
            this.pathLengthFactory = new PathLengthAStarFactory();
            GoodMoves = goodMoves;

        }

        public MinimaxResult DoMinimax(int depth, bool isComputer)
        {
            
            // megmondja, hogy ki a jatekos, ha a gep, akkor az isComputerben true van, es akkor a plyaerben xPlayer lesz.
            Occupied player = isComputer.ToPlayer();
            // a gep az alpha
            this.alpha = MoveScoreConverter.ConvertWin(player.Opponent(), 0);
            // ember a beta
            this.beta = MoveScoreConverter.ConvertWin(player, 0);
            // a minimax algoritmus magaban
            MinimaxResult bestMove = this.MiniMaxAlg(depth, isComputer, board);
            if (bestMove.Move != Location.Null)
            {
                // killer heurisztika
                GoodMoves.AddGoodMove(0, bestMove.Move);
            }
            return bestMove;
        }

        private MinimaxResult MiniMaxAlg(int depth, bool isComputer, HexBoard board)
        {
            BoardCache boardCache = new BoardCache(board.Size);
            MinimaxResult bestResult = null;
            Location cutOffMove = Location.Null;
            // az ures cellakat tartalmazza
            var possibleMoves = candidateMovesFinder.CandidateMoves(board, depth);
            
            foreach(Location move in possibleMoves)
            {
                if (!move.IsNull())
                {
                    // nem az eredetit modositom meg, hanem letrehozok egyet a peldajara
                    HexBoard board1 = new HexBoard(board.Size);
                    board1.CopyStateFrom(board);
                    board1.PlayMove(move, isComputer);

                    MinimaxResult moveScore;
                    // itt szamolja ki az allast
                    PathLengthBase staticAnalysis = this.pathLengthFactory.CreatePathLength(board1);
                    int situationScore = staticAnalysis.SituationScore();

                    if (depth<=1 || MoveScoreConverter.IsWin(situationScore))
                    {
                        moveScore = new MinimaxResult(situationScore);
                    }
                    else
                    {
                        if (depth > 1)
                        {
                            // rekurzio
                            moveScore = MiniMaxAlg(depth--, !isComputer, board1);
                            moveScore.MoveWins();
                        }
                    }
                   
                    moveScore.Move = move;
                    // Itt nezem meg, hogy a minimum kell nekunk, vagy a maximum
                    if (bestResult == null || MoveScoreConverter.MinOrMax(moveScore.Score, bestResult.Score, isComputer))
                    {
                        bestResult = new MinimaxResult(move, moveScore);
                    }

                    alpha = CheckAlpha( moveScore.Score, isComputer);
                    if (IsAlphaBetaCutoff(isComputer))
                    {
                        cutOffMove = move;
                        bestResult.Score = alpha;
                        break;
                    }

                }
                else
                {
                    break;
                }
            }
            if (bestResult != null)
            {
                GoodMoves.AddGoodMove(depth, bestResult.Move);
            }

            if (cutOffMove != Location.Null)
            {
                GoodMoves.AddGoodMove(depth, cutOffMove);
            }

            return bestResult;

        }

        private  int CheckAlpha(int testAlpha, bool isComputer)
        {
            if (isComputer && testAlpha > this.alpha)
            {
                return testAlpha;
            }

            if (!isComputer && testAlpha < this.alpha)
            {
                return testAlpha;
            }

            return this.alpha;
        }

        private bool IsAlphaBetaCutoff(bool isComputer)
        {
            if (isComputer)
            {
                return beta <= alpha;
            }

            return alpha <= beta;
        }
    }
}
