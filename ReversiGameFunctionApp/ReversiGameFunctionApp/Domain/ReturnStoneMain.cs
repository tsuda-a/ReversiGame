using Newtonsoft.Json.Linq;
using ReversiGameFunctionApp.Domain.Behaviours;
using ReversiGameFunctionApp.Models;

namespace ReversiGameFunctionApp.Domain
{
    public class ReturnStoneMain
    {
        private StoneModel _setBoard;
        private ReversiGameBoard _reversiGameBoard;

        /// <summary>
        /// New
        /// </summary>
        /// <param name="requestBody">���N�G�X�g�{�f�B</param>
        public ReturnStoneMain(string requestBody)
        {
            JObject jsonObject = JObject.Parse(requestBody);

            _setBoard = GetSetBoard(jsonObject);
            _reversiGameBoard = new ReversiGameBoard(jsonObject);
        }

        /// <summary>
        /// ���C������
        /// </summary>
        /// <returns>�Ђ�����Ԃ�����̔Ֆ�</returns>
        public string DoProcess()
        {
            var result = _reversiGameBoard.PutStone(_setBoard.Row, _setBoard.Col, _setBoard.Status);
            var turnedBoard = _reversiGameBoard.GetBoard();

            return JsonHelper.Convert<StoneModel>(turnedBoard);
        }

        /// <summary>
        /// �Z�b�g�����Ֆʂ��擾
        /// </summary>
        /// <param name="requestBody">���N�G�X�g�{�f�B</param>
        /// <returns></returns>
        private StoneModel GetSetBoard(JObject jsonObject)
        {
            JObject? setBoardJsonObject = jsonObject["setBoard"] as JObject;
            if (setBoardJsonObject == null)
            {
                throw new ArgumentException("The 'setBoard' key is missing or is null.");
            }
            var setBoardJson = JObject.Parse(setBoardJsonObject.ToString());

            return new FromJsonToModelConverter().ConvertToBoardModel(setBoardJson);
        }
    }
}
