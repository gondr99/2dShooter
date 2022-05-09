using System;
using UnityEngine;

public class UIManager
{
    public static UIManager Instance = null;

    private RectTransform _tooltipCanvasTrm = null;
    private MessageTooltip _messageTooltip = null;

    public UIManager()
    {
        try
        {
            _tooltipCanvasTrm = GameObject.Find("TooltipCanvas").GetComponent<RectTransform>();
            _messageTooltip = _tooltipCanvasTrm.Find("MessageTooltip").GetComponent<MessageTooltip>();

            _messageTooltip.CloseImmediatly(); //�����ϸ� �ٷ� �ݾƹ�����
        }catch(NullReferenceException ne)
        {
            Debug.LogError("������ ĵ������ �������� �ʽ��ϴ�. Ȯ�ο��");
            Debug.Log(ne.Message);
        }
        
    }

    public void OpenMessageTooltip(string msg, float time = 0)
    {
        _messageTooltip.ShowText(msg, time);
    }

    public void CloseMessageTooltip()
    {
        _messageTooltip.CloseText();
    }
}
