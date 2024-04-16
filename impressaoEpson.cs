using System.IO.Ports;

namespace app_pdv;

public class impressaoEpson
{
    SerialPort _serialPort;

    public impressaoEpson(string Porta, int Velocidade)
    {
        _serialPort = new SerialPort(Porta, Velocidade);
    }

    public void CortarPapel()
    {
        _serialPort.Open();
        byte[] paperCutCommand = { 27, 105 };
        _serialPort.Write(paperCutCommand, 0, paperCutCommand.Length);
        _serialPort.Close();
    }
    public void TamanhoFonte(byte tam = 00)
    {
        // https://download4.epson.biz/sec_pubs/pos/reference_en/escpos/gs_exclamation.html

        // 0(normal) a 112 Maior
        _serialPort.Open();
        byte[] command = new byte[] { 29, 33, tam }; // largura
        _serialPort.Write(command, 0, command.Length);
        _serialPort.Close();
    }

    public void TamanhoNormal()
    {
        _serialPort.Open();
        byte[] command = new byte[] { 29, 33, 0 }; // largura
        _serialPort.Write(command, 0, command.Length);
        _serialPort.Close();
    }

    public void Tamanho16() // mais pra titulo
    {
        _serialPort.Open();
        byte[] command = new byte[] { 29, 33, 16 }; // largura
        _serialPort.Write(command, 0, command.Length);
        _serialPort.Close();
    }

    public void MenorFonte(byte i = 0)
    {
        _serialPort.Open();
        byte[] smallFontCommand = { 27, 33, i }; 
        _serialPort.Write(smallFontCommand, 0, smallFontCommand.Length);
        _serialPort.Close();
    }

    public void Divisor(string marcador = "-")
    {
        int tamanho = 64;
        string linha = "";
        for (int i = 0; i < tamanho; i++)
        {
            linha += marcador;
        }

        _serialPort.Open();
        byte[] smallFontCommand = { 27, 33, 1 };
        _serialPort.Write(smallFontCommand, 0, smallFontCommand.Length);
        _serialPort.WriteLine(linha);
        byte[] smallFontCommand0 = { 27, 33, 0 };
        _serialPort.Write(smallFontCommand0, 0, smallFontCommand.Length);
        _serialPort.Close();
    }

    public void EnviarTexto(string Texto)
    {
        _serialPort.Open();
        _serialPort.WriteLine(Texto);
        _serialPort.Close();
    }

    public void AvancarLinha(byte linhas = 1)
    {
        _serialPort.Open();
        byte[] lineFeedCommand = { 27, 100, linhas };
        _serialPort.Write(lineFeedCommand, 0, lineFeedCommand.Length);
        _serialPort.Close();
    }


    public void Alinhamento(string alinhamento = "esquerdo")
    {
        byte align = 0;

        switch (alinhamento.ToLower()){
            case "esquerdo": align = 0; break;
            case "centro":   align = 1; break;
            case "direito":  align = 2; break;
        }

        _serialPort.Open();
        byte[] alignCenterCommand = { 27, 97, align };
        _serialPort.Write(alignCenterCommand, 0, alignCenterCommand.Length);
        _serialPort.Close();
    }

    public void Negrito(byte i = 0)
    {
        // 1 >  ligado
        // 0 >  desligado
        _serialPort.Open();
        byte[] boldCommand = { 27, 69, i };

        _serialPort.Write(boldCommand, 0, boldCommand.Length);
        _serialPort.Close();
    }

    public void Sublinhado(byte i = 0)
    {
        // 1 >  ligado
        // 0 >  desligado
        _serialPort.Open();
        byte[] underlineCommand = { 27, 45, 1 };
        _serialPort.Write(underlineCommand, 0, underlineCommand.Length);
        _serialPort.Close();
    }

    public void Texto(string align, string size, string texto)
    {
        switch (align)
        {
            case "e": Alinhamento("esquerdo"); break;
            case "c": Alinhamento("centro"); break;
            case "d": Alinhamento("direito"); break;
        }

        switch (size)
        {
            case "n": TamanhoNormal(); break;
            case "g": Tamanho16(); break;
        }

        _serialPort.Open();
        _serialPort.WriteLine(texto);
        _serialPort.Close();
    }

}
