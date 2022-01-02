using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class showText : MonoBehaviour
{

    public string textValue;
    public Text textElement;

    private metroSignScript MSS;

    private ScriptEntrata SE;

    public bool HoGiaIlBiglietto;

    public bool Card;

    public bool CaOn = false;

    private IntentRecognition IRC;

    private HelloWorld SHP2;

    private ticketMachineScript IMT0;

    private ScriptImageTarget1 IMT1;

    private ScriptImageTarget2 IMT2;

    private ScriptImageTarget3 IMT3;

    private ScriptImageTarget4 IMT4;

    private ScriptImageTarget5_Cash IMT5_Cash;

    private ScriptImageTarget5_Card IMT5_Card;

    private ScriptImageTarget6 IMT6;

    private ScriptImageTarget7 IMT7;

    private ScriptImageTargetTicket IMTT;

    private turnstilesScript TS;

    private DiscesaScaleMobiliScript DSM;

    private Binario1_Bignami2 B1B2;

    private ScriptBinario1_Bignami B1B;

    private ScriptBinario2_SanSiro B2SS;

    //se si va verso bignami come direzione la variabile versoBignami è true, se si va verso San Siro è false
    public bool versoBignami;

    private PortaExtMetroScript PEM;

    private PortaIntMetroScript PIM;

    private UscitaScript USC;

    private turnstilesExitScript TSE;

    private UscitaFinaleScript UFS;

    private metroSignExit MSE;



    private Page8Script page8;

    private Page5Script page5;

    private Page6Script page6;

    private ShowImages Images;








    // Update is called once per frame
    void Update()
    {

    page5 = GameObject.FindObjectOfType<Page5Script>();
    HoGiaIlBiglietto = page5.StatoTickett();

    page6 = GameObject.FindObjectOfType<Page6Script>();
    Card = page6.StatoCard();

    page8 = GameObject.FindObjectOfType<Page8Script>();
    versoBignami = page8.StatoVersoBignami();

    Images = GameObject.FindObjectOfType<ShowImages>();


    textValue = "Hello! I'm your assistant. Find the metro sign";
    textElement.text=textValue;
        //Images.buttonMetroSignTrue();

        IRC = GameObject.FindObjectOfType<IntentRecognition>();
        string CaPhrase = IRC.PassPhrase();

        SHP2 = GameObject.FindObjectOfType<HelloWorld>();
        CaOn = SHP2.CaOnOff();

        //if (ButtonMicrophone.activeSelf)
         if (CaOn == true)
        {
            textElement.text = CaPhrase;
        }

        else
        {

            MSS = GameObject.FindObjectOfType<metroSignScript>();
            bool statoMetroSign = MSS.StatusMetroSign();

            if (statoMetroSign == true)
            {

                textValue = MSS.StringaMetroSign();
                textElement.text = textValue;
                //Images.buttonMetroSignFalse();

            }

            SE = GameObject.FindObjectOfType<ScriptEntrata>();
            bool statoEntrata = SE.StatusEntrata();

            if (statoEntrata == true)
            {

                textValue = SE.StringaEntrata();
                textElement.text = textValue;
                statoMetroSign = false;
                //Images.buttonTicketMachineTrue();
            }

            IMT0 = GameObject.FindObjectOfType<ticketMachineScript>();
            bool stato0 = IMT0.Status();

            if (stato0 == true)
            {

                textValue = IMT0.Stringa0();
                textElement.text = textValue;
                statoEntrata = false;
                //Images.buttonTicketMachineFalse();
            }

            IMT1 = GameObject.FindObjectOfType<ScriptImageTarget1>();
            bool stato1 = IMT1.Status1();


            if (stato1 == true)
            {

                textValue = IMT1.Stringa1();
                textElement.text = textValue;
                stato0 = false;
            }

            IMT2 = GameObject.FindObjectOfType<ScriptImageTarget2>();
            bool stato2 = IMT2.Status2();


            if (stato2 == true)
            {

                textValue = IMT2.Stringa2();
                textElement.text = textValue;
                stato1 = false;
            }

            IMT3 = GameObject.FindObjectOfType<ScriptImageTarget3>();
            bool stato3 = IMT3.Status3();


            if (stato3 == true)
            {

                textValue = IMT3.Stringa3();
                textElement.text = textValue;
                stato2 = false;
            }

            IMT4 = GameObject.FindObjectOfType<ScriptImageTarget4>();
            bool stato4 = IMT4.Status4();


            if (stato4 == true)
            {

                textValue = IMT4.Stringa4();
                textElement.text = textValue;
                stato3 = false;
            }

            IMT5_Card = GameObject.FindObjectOfType<ScriptImageTarget5_Card>();
            bool stato5_Card = IMT5_Card.Status5_Card();
            IMT5_Cash = GameObject.FindObjectOfType<ScriptImageTarget5_Cash>();
            bool stato5_Cash = IMT5_Cash.Status5_Cash();

            if (stato5_Card == true && Card == true)
            {

                textValue = IMT5_Card.Stringa5_Card();
                textElement.text = textValue;
                stato4 = false;
            }
            else if (stato5_Cash == true && Card == false)
            {

                textValue = IMT5_Cash.Stringa5_Cash();
                textElement.text = textValue;
                stato4 = false;
            }

            IMT6 = GameObject.FindObjectOfType<ScriptImageTarget6>();
            bool stato6 = IMT6.Status6();


            if (stato6 == true)
            {

                textValue = IMT6.Stringa6();
                textElement.text = textValue;
                stato5_Cash = false;
                stato5_Card = false;
            }

            IMT7 = GameObject.FindObjectOfType<ScriptImageTarget7>();
            bool stato7 = IMT7.Status7();


            if (stato7 == true)
            {

                textValue = IMT7.Stringa7();
                textElement.text = textValue;
                stato6 = false;
            }

            IMTT = GameObject.FindObjectOfType<ScriptImageTargetTicket>();
            bool statoTicket = IMTT.StatusTicket();


            if (statoTicket == true)
            {

                textValue = IMTT.StringaTicket();
                textElement.text = textValue;
                stato7 = false;
                //Images.buttonTurnstileTrue();
            }

            TS = GameObject.FindObjectOfType<turnstilesScript>();
            bool statoTurnstiles = TS.StatusTurn();


            if (statoTurnstiles == true)
            {

                textValue = TS.StringaTurn();
                textElement.text = textValue;
                statoTicket = false;
                // Images.buttonTurnstileFalse();
            }

            DSM = GameObject.FindObjectOfType<DiscesaScaleMobiliScript>();
            bool statoScaleMobili = DSM.StatusScaleMobili();


            if (statoScaleMobili == true)
            {

                textValue = DSM.StringaScaleMobili();
                textElement.text = textValue;
                statoTurnstiles = false;
            }

            B1B2 = GameObject.FindObjectOfType<Binario1_Bignami2>();
            bool statoBignami2 = B1B2.StatusBignami2();

            if (statoBignami2 == true && versoBignami == true)
            {

                textValue = B1B.StringaBignami();
                textElement.text = textValue;
                statoScaleMobili = false;
            }





            B1B = GameObject.FindObjectOfType<ScriptBinario1_Bignami>();
            bool statoBignami = B1B.StatusBignami();
            B2SS = GameObject.FindObjectOfType<ScriptBinario2_SanSiro>();
            bool statoSanSiro = B2SS.StatusSanSiro();


            if (statoBignami == true && versoBignami == true)
            {

                textValue = B1B.StringaBignami();
                textElement.text = textValue;
                statoScaleMobili = false;
            }
            else if (statoSanSiro == true && versoBignami == false)
            {

                textValue = B2SS.StringaSanSiro();
                textElement.text = textValue;
                statoScaleMobili = false;
            }

            PEM = GameObject.FindObjectOfType<PortaExtMetroScript>();
            bool statoPortaExtMetro = PEM.StatusPortaExtMetro();


            if (statoPortaExtMetro == true)
            {

                textValue = PEM.StringaPortaExtMetro();
                textElement.text = textValue;
                statoSanSiro = false;
                statoBignami = false;
            }

            PIM = GameObject.FindObjectOfType<PortaIntMetroScript>();
            bool statoPortaIntMetro = PIM.StatusPortaIntMetro();


            if (statoPortaIntMetro == true)
            {

                textValue = PIM.StringaPortaIntMetro();
                textElement.text = textValue;
                statoPortaExtMetro = false;
            }

            USC = GameObject.FindObjectOfType<UscitaScript>();
            bool statoUscita = USC.StatusExit();


            if (statoUscita == true)
            {

                textValue = USC.StringaExit();
                textElement.text = textValue;
                statoPortaIntMetro = false;
            }

            TSE = GameObject.FindObjectOfType<turnstilesExitScript>();
            bool statoTurnstilesExit = TSE.StatusTurnstilesExit();


            if (statoTurnstilesExit == true)
            {

                textValue = TSE.StringaTurnstilesExit();
                textElement.text = textValue;
                statoUscita = false;
            }

            UFS = GameObject.FindObjectOfType<UscitaFinaleScript>();
            bool statoUscitaFinale = UFS.StatusUscitaFinale();


            if (statoUscitaFinale == true)
            {

                textValue = UFS.StringaUscitaFinale();
                textElement.text = textValue;
                statoTurnstiles = false;
            }

 /*           MSE = GameObject.FindObjectOfType<metroSignExit>();
            bool statoMetroSignExit = MSE.StatusMetroSignExit();


            if (statoMetroSignExit == true)
            {

                textValue = MSE.StringaMetroSignExit();
                textElement.text = textValue;
                statoUscitaFinale = false;
            }

*/

        }












    }
}

/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class showText : MonoBehaviour
{

    public string textValue;
    public Text textElement;

    private metroSignScript MSS;

    private ScriptEntrata SE;

    public bool HoGiaIlBiglietto;

    public bool Card;

    private ticketMachineScript IMT0;

    private ScriptImageTarget1 IMT1;

    private ScriptImageTarget2 IMT2;

    private ScriptImageTarget3 IMT3;

    private ScriptImageTarget4 IMT4;

    private ScriptImageTarget5_Cash IMT5_Cash;

    private ScriptImageTarget5_Card IMT5_Card;

    private ScriptImageTarget6 IMT6;

    private ScriptImageTarget7 IMT7;

    private ScriptImageTargetTicket IMTT;

    private turnstilesScript TS;

    private DiscesaScaleMobiliScript DSM;

    private ScriptBinario1_Bignami B1B;

    private Binario1_Bignami2 B1B2;

    private ScriptBinario2_SanSiro B2SS;
    //se si va verso bignami come direzione la variabile versoBignami è true, se si va verso San Siro è false
    public bool versoBignami;

    private PortaExtMetroScript PEM;

    private PortaIntMetroScript PIM;

    private UscitaScript USC;

    private turnstilesExitScript TSE;

    private UscitaFinaleScript UFS;

    private metroSignExit MSE;






    // Update is called once per frame
    void Update()
    {

    MSS = GameObject.FindObjectOfType<metroSignScript>();
    bool statoMetroSign = MSS.StatusMetroSign();
    SE = GameObject.FindObjectOfType<ScriptEntrata>();
    bool statoEntrata = SE.StatusEntrata();
    IMT0 = GameObject.FindObjectOfType<ticketMachineScript>();
    bool stato0 = IMT0.Status();
    IMT1 = GameObject.FindObjectOfType<ScriptImageTarget1>();
    bool stato1 = IMT1.Status1();
     IMT2 = GameObject.FindObjectOfType<ScriptImageTarget2>();
     bool stato2 = IMT2.Status2();
     IMT3 = GameObject.FindObjectOfType<ScriptImageTarget3>();
     bool stato3 = IMT3.Status3();
     IMT4 = GameObject.FindObjectOfType<ScriptImageTarget4>();
     bool stato4 = IMT4.Status4();
     IMT5_Card = GameObject.FindObjectOfType<ScriptImageTarget5_Card>();
     bool stato5_Card = IMT5_Card.Status5_Card();
     IMT5_Cash = GameObject.FindObjectOfType<ScriptImageTarget5_Cash>();
     bool stato5_Cash = IMT5_Cash.Status5_Cash();
    IMT6 = GameObject.FindObjectOfType<ScriptImageTarget6>();
    bool stato6 = IMT6.Status6();
    IMT7 = GameObject.FindObjectOfType<ScriptImageTarget7>();
    bool stato7 = IMT7.Status7();
    IMTT = GameObject.FindObjectOfType<ScriptImageTargetTicket>();
    bool statoTicket = IMTT.StatusTicket();
    TS = GameObject.FindObjectOfType<turnstilesScript>();
    bool statoTurnstiles = TS.StatusTurn();
    DSM = GameObject.FindObjectOfType<DiscesaScaleMobiliScript>();
    bool statoScaleMobili = DSM.StatusScaleMobili();
    B1B = GameObject.FindObjectOfType<ScriptBinario1_Bignami>();
    bool statoBignami = B1B.StatusBignami();
    B2SS = GameObject.FindObjectOfType<ScriptBinario2_SanSiro>();
    bool statoSanSiro = B2SS.StatusSanSiro();
    B1B2 = GameObject.FindObjectOfType<Binario1_Bignami2>();
    bool statoBignami2 = B1B2.StatusBignami2();
    PEM = GameObject.FindObjectOfType<PortaExtMetroScript>();
    bool statoPortaExtMetro = PEM.StatusPortaExtMetro();
    PIM = GameObject.FindObjectOfType<PortaIntMetroScript>();
    bool statoPortaIntMetro = PIM.StatusPortaIntMetro();
    USC = GameObject.FindObjectOfType<UscitaScript>();
    bool statoUscita = USC.StatusExit();
    TSE = GameObject.FindObjectOfType<turnstilesExitScript>();
    bool statoTurnstilesExit = TSE.StatusTurnstilesExit();
    UFS = GameObject.FindObjectOfType<UscitaFinaleScript>();
    bool statoUscitaFinale = UFS.StatusUscitaFinale();
    MSE = GameObject.FindObjectOfType<metroSignExit>();
    bool statoMetroSignExit = MSE.StatusMetroSignExit();


        if (statoMetroSign == true && statoEntrata == false && stato0 == false){

            textValue=MSS.StringaMetroSign();
            textElement.text=textValue;


        }



        if (statoMetroSign == true && statoEntrata == true && stato0 == false){

            textValue=SE.StringaEntrata();
            textElement.text=textValue;

        }




        if (statoMetroSign == true && statoEntrata == false && stato0 == true && HoGiaIlBiglietto == false && stato1 == false){

            textValue=IMT0.Stringa0();
            textElement.text=textValue;

        }


          if (stato0 == true && stato1 == true && stato2 == false){

                textValue=IMT1.Stringa1();
                textElement.text=textValue;


          }



           if (stato1 == true && stato2 == true && stato3 == false){

                 textValue=IMT2.Stringa2();
                 textElement.text=textValue;

           }



           if (stato2 == true && stato3 == true && stato4 == false){

                 textValue=IMT3.Stringa3();
                 textElement.text=textValue;


           }



           if (stato3 == true && stato4 == true ){

                 textValue=IMT4.Stringa4();
                 textElement.text=textValue;


           }



           if (stato4 == true && stato5_Card == true && Card == true && stato6 == false){

                 textValue=IMT5_Card.Stringa5_Card();
                 textElement.text=textValue;


           }
           if (stato4 == true && stato5_Cash == true && Card == false && stato6 == false){

                 textValue=IMT5_Cash.Stringa5_Cash();
                 textElement.text=textValue;


           }



          if (stato6 == true){

                textValue=IMT6.Stringa6();
                textElement.text=textValue;



          }



          if (stato7 == true){

                textValue=IMT7.Stringa7();
                textElement.text=textValue;


          }



          if (statoTicket == true){

                textValue=IMTT.StringaTicket();
                textElement.text=textValue;


          }



          if (statoTurnstiles == true){

                textValue=TS.StringaTurn();
                textElement.text=textValue;


          }



          if (statoScaleMobili == true){

                textValue=DSM.StringaScaleMobili();
                textElement.text=textValue;


          }




          if (statoBignami == true && versoBignami == true){

                textValue=B1B.StringaBignami();
                textElement.text=textValue;


          }



          if (statoSanSiro == true && versoBignami == false){

                textValue=B2SS.StringaSanSiro();
                textElement.text=textValue;


          }



            if (statoBignami2 == true && versoBignami == true){

                            textValue=B1B2.StringaBignami2();
                            textElement.text=textValue;


                      }



          if (statoPortaExtMetro == true){

                textValue=PEM.StringaPortaExtMetro();
                textElement.text=textValue;



          }



          if (statoPortaIntMetro == true){

                textValue=PIM.StringaPortaIntMetro();
                textElement.text=textValue;


          }



          if (statoUscita == true){

                textValue=USC.StringaExit();
                textElement.text=textValue;


          }



          if (statoTurnstilesExit == true){

                textValue=TSE.StringaTurnstilesExit();
                textElement.text=textValue;


          }



          if (statoUscitaFinale == true){

                textValue=UFS.StringaUscitaFinale();
                textElement.text=textValue;


          }




          if (statoMetroSignExit == true){

                textValue=MSE.StringaMetroSignExit();
                textElement.text=textValue;


          }



                statoMetroSign = false;
                statoEntrata = false;
                stato0 = false;
                statoTicket = false;
                statoTurnstiles = false;
                statoScaleMobili = false;
                statoBignami = false;
                statoSanSiro = false;
                statoBignami2 = false;
                statoPortaExtMetro = false;
                statoPortaIntMetro = false;
                statoUscita = false;
                statoTurnstilesExit = false;
                statoUscitaFinale = false;
                statoMetroSignExit = false;














    }
}
*/