namespace Othello.Domain.Model
{
    public interface IAI
    {
        /// <summary>
        /// 合法手が存在するかどうかは事前にチェックすること
        /// <para>(実装側では合法手が存在しない場合例外を投げることとする)</para>
        /// </summary>
        /// <param name="teban"></param>
        /// <param name="board"></param>
        /// <returns></returns>
        Position DecideMove(Teban teban, IBoard board);
    }
}