//声明命名空间
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
//需要添加以下命名空间
using UFIDA.U8.MomServiceCommon;
using UFIDA.U8.U8MOMAPIFramework;
using UFIDA.U8.U8APIFramework;  // 选择.NETFramework 2.0
using UFIDA.U8.U8APIFramework.Meta;
using UFIDA.U8.U8APIFramework.Parameter;
using MSXML2;
using System.Xml;
using Yaxon.ErpAPI.Common;
using Yaxon.ErpAPI.Models;

namespace Yaxon.ErpAPI.U8API.ArrivedGoods
{
    public class VoucherSave
    {
        public U8ReturnMessage Save()
        {

            try
            {
                //第一步：构造u8login对象并登陆(引用U8API类库中的Interop.U8Login.dll)
                //如果当前环境中有login对象则可以省去第一步
                U8Login.clsLogin u8Login = new U8Login.clsLogin();
                String sSubId = "AS";
                String sAccID = "(default)@999";/*必须这样写*/
                String sYear = "2018";
                String sUserID = "3089";
                String sPassword = "wxz123456";
                String sDate = "2018-01-31";
                String sServer = "erp";
                String sSerial = "";
                int a = u8Login.LogState;
                if (!u8Login.Login(ref sSubId, ref sAccID, ref sYear, ref sUserID, ref sPassword, ref sDate, ref sServer, ref sSerial))
                {
                    //Console.WriteLine("登陆失败，原因：" + u8Login.ShareString);
                    Marshal.FinalReleaseComObject(u8Login);
                    LogHelper.ErrorLog("【U8API/ArrivedGoods/VoucherSave(采购订单到货单)】登陆失败，原因：" +
                                                                                                    u8Login.ShareString, null);
                    U8ReturnMessage result = new U8ReturnMessage(0, "【U8API/ArrivedGoods/VoucherSave(采购订单到货单)】登陆失败，原因：" + u8Login.ShareString, "", "0");
                    return result;
                }

                XmlDocument xmlDoc = new XmlDocument();
                XmlDocument xmlHeadDoc = new XmlDocument();
                XmlDocument xmlBodyDoc = new XmlDocument();

                #region xmlDoc

                string xmlHeadStr = @"<U8API>
            <ArrivedGoods>
            <VoucherSave>
            <DomHead>
            <id>1</id>
            <ipresent> </ipresent> 
            <cmaketime> </cmaketime> 
            <cmodifytime> </cmodifytime> 
            <cmodifydate> </cmodifydate> 
            <creviser> </creviser> 
            <caudittime> </caudittime> 
            <cauditdate> </cauditdate> 
            <cbustype>普通采购</cbustype> 
            <ccode>201802011111</ccode> 
            <ddate>2018-02-01</ddate> 
            <cptname></cptname> 
            <cvenabbname>厦门雅迅</cvenabbname> 
            <ccloser> </ccloser> 
            <iverifystateex > </iverifystateex >
            <ireturncount> </ireturncount> 
            <iswfcontrolled> </iswfcontrolled> 
            <cdepname>CRM</cdepname> 
            <cpersonname> </cpersonname> 
            <cexch_name>人民币</cexch_name> 
            <iexchrate>0.2</iexchrate>
            <cscname></cscname>
            <itaxrate> </itaxrate> 
            <cmemo> </cmemo> 
            <cmaker> </cmaker> 
            <ivtid> </ivtid> 
            <cverifier> </cverifier> 
            <bnegative> </bnegative>
            <cvencode> </cvencode> 
            <cdepcode> </cdepcode> 
            <cptcode> </cptcode> 
            <cpaycode> </cpaycode> 
            <cpersoncode> </cpersoncode> 
            <ufts> </ufts> 
            <cpayname> </cpayname> 
            <csccode ></csccode >
            <cdefine1> </cdefine1> 
            <cdefine2> </cdefine2> 
            <cdefine3> </cdefine3>
            <cdefine4> </cdefine4>
            <cdefine5> </cdefine5>
            <cdefine6> </cdefine6>
            <cdefine7> </cdefine7>
            <cdefine8> </cdefine8>
            <cdefine9> </cdefine9>
            <cdefine10> </cdefine10>
            <cdefine11> </cdefine11>
            <cdefine12> </cdefine12>
            <cdefine13> </cdefine13>
            <cdefine14> </cdefine14>
            <cdefine15> </cdefine15>
            <cdefine16> </cdefine16>
            <cvendefine1> </cvendefine1> 
            <cvendefine2> </cvendefine2> 
            <cvendefine3> </cvendefine3> 
            <cvendefine4> </cvendefine4> 
            <cvendefine5> </cvendefine5> 
            <cvendefine6> </cvendefine6> 
            <cvendefine7> </cvendefine7> 
            <cvendefine8> </cvendefine8> 
            <cvendefine9 ></cvendefine9> 
            <cvendefine10> </cvendefine10> 
            <cvendefine11></cvendefine11> 
            <cvendefine12></cvendefine12> 
            <cvendefine13> </cvendefine13> 
            <cvendefine14> </cvendefine14> 
            <cvendefine15> </cvendefine15> 
            <cvendefine16> </cvendefine16> 
            <idiscounttaxtype></idiscounttaxtype>
            <ibilltype> </ibilltype> 
            <cvenpuomprotocol> </cvenpuomprotocol>
            <cvenpuomprotocolname> </cvenpuomprotocolname>
            <cflowname>流程模式描述</cflowname>
            <cchanger>王贤忠</cchanger> 
            <iflowid>999</iflowid> 
            <cpocode>201802012222</cpocode>
            <csysbarcode>123456789</csysbarcode> 
            <csysbarcodepo>987654321</csysbarcodepo>	
            </DomHead>
            <domBody>
            <autoid>1</autoid>
            <cinvcode>1234568977</cinvcode> 
            <cinvstd></cinvstd>
            <inum> </inum>
            <iinvexchrate ></iinvexchrate>
            <ioricost></ioricost>
            <iorimoney></iorimoney>
            <ioritaxprice></ioritaxprice>
            <icost></icost>
            <imoney></imoney>
            <itaxprice></itaxprice>
            <isum></isum>
            <cinvdefine1></cinvdefine1>
            <cinvdefine4></cinvdefine4>
            <cinvdefine5></cinvdefine5>
            <cfree1></cfree1>
            <cinvdefine6></cinvdefine6>
            <cfree2></cfree2>
            <cinvdefine7></cinvdefine7>
            <iorisum></iorisum>
            <cinvdefine8></cinvdefine8>
            <cinvdefine9></cinvdefine9>
            <cdefine22></cdefine22>
            <cinvdefine10></cinvdefine10>
            <cdefine23></cdefine23>
            <cinvdefine11></cinvdefine11>
            <cdefine24></cdefine24>
            <cinvdefine12></cinvdefine12>
            <cdefine25></cdefine25>
            <cinvdefine13></cinvdefine13>
            <cdefine26></cdefine26>
            <cinvdefine14></cinvdefine14>
            <cdefine27></cdefine27>
            <cinvdefine15></cinvdefine15>
            <itaxrate></itaxrate>
            <cinvdefine16></cinvdefine16>
            <citemcode></citemcode>
            <cinvdefine2></cinvdefine2>
            <citem_class></citem_class>
            <cinvdefine3></cinvdefine3>
            <iposid></iposid>
            <citemname></citemname>
            <citem_name></citem_name>
            <cfree3></cfree3>
            <cfree4></cfree4>
            <cfree5></cfree5>
            <cfree6></cfree6>
            <cfree7></cfree7>
            <cfree8></cfree8>
            <cfree9></cfree9>
            <cfree10></cfree10>
            <corufts></corufts>
            <cunitid></cunitid>
            <cinva_unit></cinva_unit>
            <cinvm_unit></cinvm_unit>
            <igrouptype></igrouptype>
            <cdefine28></cdefine28>
            <cdefine29></cdefine29>
            <cdefine30></cdefine30>
            <cdefine31></cdefine31>
            <cdefine32></cdefine32>
            <cdefine33></cdefine33>
            <cdefine34></cdefine34>
            <cdefine35></cdefine35>
            <cdefine36></cdefine36>
            <cdefine37></cdefine37>
            <ioritaxcost></ioritaxcost>
            <iquantity>6.0</iquantity>
            <icorid></icorid>
            <cbatch></cbatch>
            <dpdate></dpdate>
            <dvdate></dvdate>
            <imassdate></imassdate>
            <bgsp></bgsp>
            <id></id>
            <cwhcode></cwhcode>
            <cwhname></cwhname>
            <cinvname></cinvname>
            <btaxcost></btaxcost>
            <binspect></binspect>
            <fvalidquantity></fvalidquantity>
            <finvalidquantity></finvalidquantity>
            <frefusequantity></frefusequantity>
            <cinvaddcode></cinvaddcode>
            <frefusenum></frefusenum>
            <cveninvcode></cveninvcode>
            <cveninvname></cveninvname>
            <bexigency></bexigency>
            <cmassunit></cmassunit>
            <ippartid></ippartid>
            <ipquantity></ipquantity>
            <iptoseq></iptoseq>
            <cordercode></cordercode>
            <contractrowno></contractrowno>
            <contractrowguid></contractrowguid>
            <contractcode></contractcode>
            <sotype></sotype>
            <csocode></csocode>
            <irowno></irowno>
            <sodid></sodid>
            <frealquantity></frealquantity>
            <frealnum></frealnum>
            <fsumrefusequantity></fsumrefusequantity>
            <fsumrefusenum></fsumrefusenum>
            <fvalidnum></fvalidnum>
            <iexpiratdatecalcu></iexpiratdatecalcu>
            <cexpirationdate></cexpirationdate>
            <dexpirationdate></dexpirationdate>
            <cbcloser></cbcloser>
            <dValidateDate></dValidateDate>
            <iinvmpcost></iinvmpcost>
            <cupsocode></cupsocode>
            <cdemandmemo></cdemandmemo>
            <iorderdid></iorderdid>
            <iordertype></iordertype>
            <csoordercode></csoordercode>
            <iorderseq></iorderseq>
            <editprop>编辑属性</editprop>
            <ivouchrowno>99</ivouchrowno>
            <cbatchproperty1>批次属性1</cbatchproperty1>
            <cbmemo>备注</cbmemo>
            <cbsysbarcode>9876512336</cbsysbarcode>
            <carrivalcode>369852147</carrivalcode>
            <irejectautoid>258741369</irejectautoid>
            <cbatchproperty2>批次属性2</cbatchproperty2>
            <cbatchproperty3>批次属性3</cbatchproperty3>
            <cbatchproperty4>批次属性4</cbatchproperty4>
            <cbatchproperty5>批次属性5</cbatchproperty5>
            <cbatchproperty6>批次属性6</cbatchproperty6>
            <cbatchproperty7>批次属性7</cbatchproperty7>
            <cbatchproperty8>批次属性8</cbatchproperty8>
            <cbatchproperty9>批次属性9</cbatchproperty9>
            <cbatchproperty10>批次属性10</cbatchproperty10>
            <isourcemocode>8258693145</isourcemocode>
            <isourcemodetailsid>456789321</isourcemodetailsid>
            <freworkquantity>1</freworkquantity>
            <freworknum>1</freworknum>
            <iproducttype>产出品类型</iproducttype>
            <cmaininvcode>1234445698</cmaininvcode>
            <imainmodetailsid>123456897985</imainmodetailsid>
            <planlotnumber>17445698774</planlotnumber>
            <bgift>哇哈哈</bgift>
            <taskguid>2586987</taskguid>
            </domBody>
            <VoucherState>2</VoucherState>
            <UserMode>0</UserMode>
            <VoucherType>2</VoucherType>
            <bPositive>True</bPositive>
            <sBillType>0</sBillType>
            <sBusType>普通采购</sBusType>
            </VoucherSave>
            </ArrivedGoods>
            </U8API>";

                xmlDoc.LoadXml(xmlHeadStr);
                #endregion

                #region xmlHeadStr
                //xmlDoc.SelectNodes("/*/*/*/DomHead");
                string InnerHeadXml = xmlDoc.SelectSingleNode("/*/*/*/DomHead").InnerXml;
                InnerHeadXml = "<DomHead>" + InnerHeadXml + "</DomHead>";
                xmlHeadDoc.LoadXml(InnerHeadXml);
                #endregion

                #region xmlBodyStr
                string InnerBodyXml = xmlDoc.SelectSingleNode("/*/*/*/domBody").InnerXml;
                InnerBodyXml = "<domBody>" + InnerBodyXml + "</domBody>";
                xmlBodyDoc.LoadXml(InnerBodyXml);
                #endregion

                #region xmlStr
                string VoucherState = xmlDoc.SelectSingleNode("/*/*/*/VoucherState").InnerText;
                string UserMode = xmlDoc.SelectSingleNode("/*/*/*/UserMode").InnerText;
                string VoucherType = xmlDoc.SelectSingleNode("/*/*/*/VoucherType").InnerText;
                string bPositive = xmlDoc.SelectSingleNode("/*/*/*/bPositive").InnerText;
                string sBillType = xmlDoc.SelectSingleNode("/*/*/*/sBillType").InnerText;
                string sBusType = xmlDoc.SelectSingleNode("/*/*/*/sBusType").InnerText;
                #endregion

                //return;
                //第二步：构造环境上下文对象，传入login，并按需设置其它上下文参数
                U8EnvContext envContext = new U8EnvContext();
                envContext.U8Login = u8Login;

                //采购所有接口均支持内部独立事务和外部事务，默认内部事务
                //如果是外部事务，则需要传递ADO.Connection对象，并将IsIndependenceTransaction属性设置为false
                //envContext.BizDbConnection = new ADO.Connection();
                //envContext.IsIndependenceTransaction = false;

                //设置上下文参数
                envContext.SetApiContext("VoucherType", 2); //上下文数据类型：int，含义：单据类型， 采购到货单 2 
                envContext.SetApiContext("bPositive", true);//上下文数据类型：bool，含义：红蓝标识：True,蓝字；False,红字
                envContext.SetApiContext("sBillType", "0"); //上下文数据类型：string，含义：到货单类型， 到货单 0 退货单 1
                envContext.SetApiContext("sBusType", "普通采购");//上下文数据类型：string，含义：业务类型：普通采购,直运采购,受托代销       

                //第三步：设置API地址标识(Url)
                //当前API：新增或修改的地址标识为：U8API/ArrivedGoods/VoucherSave
                U8ApiAddress myApiAddress = new U8ApiAddress("U8API/ArrivedGoods/VoucherSave");
                //envContext.U8AccountDataSource
                //第四步：构造APIBroker
                U8ApiBroker broker = new U8ApiBroker(myApiAddress, envContext);

                //第五步：API参数赋值

                //给BO表头参数DomHead赋值，此BO参数的业务类型为到货单，属表头参数。BO参数均按引用传递
                //提示：给BO表头参数DomHead赋值有两种方法

                //方法一是直接传入MSXML2.DOMDocumentClass对象
                //broker.AssignNormalValue("DomHead", new MSXML2.DOMDocumentClass())

                //方法二是构造BusinessObject对象，具体方法如下：
                BusinessObject DomHead = broker.GetBoParam("DomHead");

                DomHead.RowCount = 1; //设置BO对象(表头)行数，只能为一行
                //给BO对象(表头)的字段赋值，值可以是真实类型，也可以是无类型字符串
                //以下代码示例只设置第一行值。各字段定义详见API服务接口定义

                /****************************** 以下是必输字段 ****************************/
                DomHead[0]["id"] = "99999"; //主关键字段，int类型
                DomHead[0]["cbustype"] = "0"; //业务类型，int类型
                DomHead[0]["ccode"] = "180131001"; //单据号，string类型
                DomHead[0]["ddate"] = "2018-02-01 00:00:00.000"; //日期，DateTime类型
                DomHead[0]["cvenabbname"] = "北京长江恒业自动化电器设备有限公司"; //供应商，string类型
                DomHead[0]["cdepname"] = "CRM研发中心"; //部门，string类型
                DomHead[0]["cexch_name"] = "人民币"; //币种，string类型
                DomHead[0]["iexchrate"] = "17"; //汇率，double类型
                DomHead[0]["cflowname"] = "流程模式描述"; //流程模式描述，string类型
                DomHead[0]["cchanger"] = "游爱明"; //变更人，string类型
                DomHead[0]["iflowid"] = "5"; //流程ID，string类型
                DomHead[0]["cpocode"] = "180131001"; //订单号，string类型
                DomHead[0]["csysbarcode"] = "123456"; //单据条码，string类型
                DomHead[0]["csysbarcodepo"] = "258639"; //来源条形码，string类型

                /***************************** 以下是非必输字段 ****************************/
                DomHead[0]["ipresent"] = ""; //现存量，string类型
                DomHead[0]["cmaketime"] = ""; //制单时间，DateTime类型
                DomHead[0]["cmodifytime"] = ""; //修改时间，DateTime类型
                DomHead[0]["cmodifydate"] = ""; //修改日期，DateTime类型
                DomHead[0]["creviser"] = ""; //修改人，string类型
                DomHead[0]["caudittime"] = ""; //审核时间，DateTime类型
                DomHead[0]["cauditdate"] = ""; //审核日期，DateTime类型
                DomHead[0]["cptname"] = ""; //采购类型，string类型
                DomHead[0]["ccloser"] = ""; //关闭人，string类型
                DomHead[0]["iverifystateex"] = ""; //审核状态，string类型
                DomHead[0]["ireturncount"] = ""; //打回次数，string类型
                DomHead[0]["iswfcontrolled"] = ""; //是否启用工作流，string类型
                DomHead[0]["cpersonname"] = ""; //业 务 员，string类型
                DomHead[0]["cscname"] = ""; //运输方式，string类型
                DomHead[0]["itaxrate"] = ""; //税率，double类型
                DomHead[0]["cmemo"] = ""; //备注，string类型
                DomHead[0]["cmaker"] = ""; //制单人，string类型
                DomHead[0]["ivtid"] = ""; //单据模版号，int类型
                DomHead[0]["cverifier"] = ""; //审核人，string类型
                DomHead[0]["bnegative"] = ""; //负发票标志，string类型
                DomHead[0]["cvencode"] = ""; //供货单位编号，string类型
                DomHead[0]["cdepcode"] = ""; //部门编号，string类型
                DomHead[0]["cptcode"] = ""; //采购类型编码，string类型
                DomHead[0]["cpaycode"] = ""; //付款条件编码，string类型
                DomHead[0]["cpersoncode"] = ""; //职员编号，string类型
                DomHead[0]["ufts"] = ""; //时间戳，string类型
                DomHead[0]["cpayname"] = ""; //付款条件，string类型
                DomHead[0]["csccode"] = ""; //运输方式编码，string类型
                DomHead[0]["cdefine1"] = ""; //表头自定义项1，string类型
                DomHead[0]["cdefine2"] = ""; //表头自定义项2，string类型
                DomHead[0]["cdefine3"] = ""; //表头自定义项3，string类型
                DomHead[0]["cdefine4"] = ""; //表头自定义项4，DateTime类型
                DomHead[0]["cdefine5"] = ""; //表头自定义项5，int类型
                DomHead[0]["cdefine6"] = ""; //表头自定义项6，DateTime类型
                DomHead[0]["cdefine7"] = ""; //表头自定义项7，double类型
                DomHead[0]["cdefine8"] = ""; //表头自定义项8，string类型
                DomHead[0]["cdefine9"] = ""; //表头自定义项9，string类型
                DomHead[0]["cdefine10"] = ""; //表头自定义项10，string类型
                DomHead[0]["cdefine11"] = ""; //表头自定义项11，string类型
                DomHead[0]["cdefine12"] = ""; //表头自定义项12，string类型
                DomHead[0]["cdefine13"] = ""; //表头自定义项13，string类型
                DomHead[0]["cdefine14"] = ""; //表头自定义项14，string类型
                DomHead[0]["cdefine15"] = ""; //表头自定义项15，int类型
                DomHead[0]["cdefine16"] = ""; //表头自定义项16，double类型
                DomHead[0]["cvendefine1"] = ""; //供应商自定义项1，string类型
                DomHead[0]["cvendefine2"] = ""; //供应商自定义项2，string类型
                DomHead[0]["cvendefine3"] = ""; //供应商自定义项3，string类型
                DomHead[0]["cvendefine4"] = ""; //供应商自定义项4，string类型
                DomHead[0]["cvendefine5"] = ""; //供应商自定义项5，string类型
                DomHead[0]["cvendefine6"] = ""; //供应商自定义项6，string类型
                DomHead[0]["cvendefine7"] = ""; //供应商自定义项7，string类型
                DomHead[0]["cvendefine8"] = ""; //供应商自定义项8，string类型
                DomHead[0]["cvendefine9"] = ""; //供应商自定义项9，string类型
                DomHead[0]["cvendefine10"] = ""; //供应商自定义项10，string类型
                DomHead[0]["cvendefine11"] = ""; //供应商自定义项11，string类型
                DomHead[0]["cvendefine12"] = ""; //供应商自定义项12，string类型
                DomHead[0]["cvendefine13"] = ""; //供应商自定义项13，string类型
                DomHead[0]["cvendefine14"] = ""; //供应商自定义项14，string类型
                DomHead[0]["cvendefine15"] = ""; //供应商自定义项15，string类型
                DomHead[0]["cvendefine16"] = ""; //供应商自定义项16，string类型
                DomHead[0]["idiscounttaxtype"] = ""; //扣税类别，int类型
                DomHead[0]["ibilltype"] = ""; //单据类型，int类型
                DomHead[0]["cvenpuomprotocol"] = ""; //收付款协议编码，string类型
                DomHead[0]["cvenpuomprotocolname"] = ""; //收付款协议名称，string类型

                //给BO表体参数domBody赋值，此BO参数的业务类型为到货单，属表体参数。BO参数均按引用传递
                //提示：给BO表体参数domBody赋值有两种方法

                //方法一是直接传入MSXML2.DOMDocumentClass对象
                //broker.AssignNormalValue("domBody", new MSXML2.DOMDocumentClass())

                //方法二是构造BusinessObject对象，具体方法如下：
                BusinessObject domBody = broker.GetBoParam("domBody");
                domBody.RowCount = 10; //设置BO对象行数
                //可以自由设置BO对象行数为大于零的整数，也可以不设置而自动增加行数
                //给BO对象的字段赋值，值可以是真实类型，也可以是无类型字符串
                //以下代码示例只设置第一行值。各字段定义详见API服务接口定义

                /****************************** 以下是必输字段 ****************************/
                domBody[0]["autoid"] = "9999"; //主关键字段，int类型
                domBody[0]["cinvcode"] = "1000-0000-0000"; //存货编码，string类型
                domBody[0]["iquantity"] = "17"; //主计量数量，double类型
                domBody[0]["editprop"] = "A"; //编辑属性：A表新增，M表修改，D表删除，string类型
                domBody[0]["ivouchrowno"] = "99"; //行号，string类型
                domBody[0]["cbatchproperty1"] = "1"; //批次属性1，string类型
                domBody[0]["cbmemo"] = "哈哈哈"; //备注，string类型
                domBody[0]["cbsysbarcode"] = "99"; //单据行条码，string类型
                domBody[0]["carrivalcode"] = "201802011278"; //到货单号，string类型
                domBody[0]["irejectautoid"] = "123456"; //irejectautoid，string类型
                domBody[0]["cbatchproperty2"] = "2"; //批次属性2，string类型
                domBody[0]["cbatchproperty3"] = "3"; //批次属性3，string类型
                domBody[0]["cbatchproperty4"] = "4"; //批次属性4，string类型
                domBody[0]["cbatchproperty5"] = "5"; //批次属性5，string类型
                domBody[0]["cbatchproperty6"] = "6"; //批次属性6，string类型
                domBody[0]["cbatchproperty7"] = "7"; //批次属性7，string类型
                domBody[0]["cbatchproperty8"] = "8"; //批次属性8，string类型
                domBody[0]["cbatchproperty9"] = "9"; //批次属性9，string类型
                domBody[0]["cbatchproperty10"] = "10"; //批次属性10，string类型
                domBody[0]["isourcemocode"] = "258654789"; //源订单号，string类型
                domBody[0]["isourcemodetailsid"] = "1234567"; //源订单子表ID，string类型
                domBody[0]["freworkquantity"] = "1"; //返工数量，string类型
                domBody[0]["freworknum"] = "1"; //返工件数，string类型
                domBody[0]["iproducttype"] = "1"; //产出品类型，string类型
                domBody[0]["cmaininvcode"] = "1000-0000-0000"; //主产品存货编码，string类型
                domBody[0]["imainmodetailsid"] = "123456"; //主产品订单子表ID，string类型
                domBody[0]["planlotnumber"] = "20180201"; //计划批号，string类型
                domBody[0]["bgift"] = "1"; //赠品，string类型
                domBody[0]["taskguid"] = "00065B8F-0000-0001-0000-0000F4AB4137"; //taskguid，string类型

                /***************************** 以下是非必输字段 ****************************/
                domBody[0]["cinvstd"] = ""; //规格型号，string类型
                domBody[0]["inum"] = ""; //件数，double类型
                domBody[0]["iinvexchrate"] = ""; //换算率，double类型
                domBody[0]["ioricost"] = ""; //原币单价，double类型
                domBody[0]["iorimoney"] = ""; //原币金额，double类型
                domBody[0]["ioritaxprice"] = ""; //原币税额，double类型
                domBody[0]["icost"] = ""; //本币单价，double类型
                domBody[0]["imoney"] = ""; //本币金额，double类型
                domBody[0]["itaxprice"] = ""; //本币税额，double类型
                domBody[0]["isum"] = ""; //本币价税合计，double类型
                domBody[0]["cinvdefine1"] = ""; //存货自定义项1，string类型
                domBody[0]["cinvdefine4"] = ""; //存货自定义项4，string类型
                domBody[0]["cinvdefine5"] = ""; //存货自定义项5，string类型
                domBody[0]["cfree1"] = ""; //自由项1，string类型
                domBody[0]["cinvdefine6"] = ""; //存货自定义项6，string类型
                domBody[0]["cfree2"] = ""; //自由项2，string类型
                domBody[0]["cinvdefine7"] = ""; //存货自定义项7，string类型
                domBody[0]["iorisum"] = ""; //原币价税合计，double类型
                domBody[0]["cinvdefine8"] = ""; //存货自定义项8，string类型
                domBody[0]["cinvdefine9"] = ""; //存货自定义项9，string类型
                domBody[0]["cdefine22"] = ""; //表体自定义项1，string类型
                domBody[0]["cinvdefine10"] = ""; //存货自定义项10，string类型
                domBody[0]["cdefine23"] = ""; //表体自定义项2，string类型
                domBody[0]["cinvdefine11"] = ""; //存货自定义项11，string类型
                domBody[0]["cdefine24"] = ""; //表体自定义项3，string类型
                domBody[0]["cinvdefine12"] = ""; //存货自定义项12，string类型
                domBody[0]["cdefine25"] = ""; //表体自定义项4，string类型
                domBody[0]["cinvdefine13"] = ""; //存货自定义项13，string类型
                domBody[0]["cdefine26"] = ""; //表体自定义项5，double类型
                domBody[0]["cinvdefine14"] = ""; //存货自定义项14，string类型
                domBody[0]["cdefine27"] = ""; //表体自定义项6，double类型
                domBody[0]["cinvdefine15"] = ""; //存货自定义项15，string类型
                domBody[0]["itaxrate"] = ""; //税率，double类型
                domBody[0]["cinvdefine16"] = ""; //存货自定义项16，string类型
                domBody[0]["citemcode"] = ""; //项目编码，string类型
                domBody[0]["cinvdefine2"] = ""; //存货自定义项2，string类型
                domBody[0]["citem_class"] = ""; //项目大类编码，string类型
                domBody[0]["cinvdefine3"] = ""; //存货自定义项3，string类型
                domBody[0]["iposid"] = ""; //订单子表ID，int类型
                domBody[0]["citemname"] = ""; //项目名称，string类型
                domBody[0]["citem_name"] = ""; //项目大类名称，string类型
                domBody[0]["cfree3"] = ""; //自由项3，string类型
                domBody[0]["cfree4"] = ""; //自由项4，string类型
                domBody[0]["cfree5"] = ""; //自由项5，string类型
                domBody[0]["cfree6"] = ""; //自由项6，string类型
                domBody[0]["cfree7"] = ""; //自由项7，string类型
                domBody[0]["cfree8"] = ""; //自由项8，string类型
                domBody[0]["cfree9"] = ""; //自由项9，string类型
                domBody[0]["cfree10"] = ""; //自由项10，string类型
                domBody[0]["corufts"] = ""; //对应单据时间戳，string类型
                domBody[0]["cunitid"] = ""; //单位编码，string类型
                domBody[0]["cinva_unit"] = ""; //采购单位，string类型
                domBody[0]["cinvm_unit"] = ""; //主计量，string类型
                domBody[0]["igrouptype"] = ""; //分组类型，string类型
                domBody[0]["cdefine28"] = ""; //表体自定义项7，string类型
                domBody[0]["cdefine29"] = ""; //表体自定义项8，string类型
                domBody[0]["cdefine30"] = ""; //表体自定义项9，string类型
                domBody[0]["cdefine31"] = ""; //表体自定义项10，string类型
                domBody[0]["cdefine32"] = ""; //表体自定义项11，string类型
                domBody[0]["cdefine33"] = ""; //表体自定义项12，string类型
                domBody[0]["cdefine34"] = ""; //表体自定义项13，int类型
                domBody[0]["cdefine35"] = ""; //表体自定义项14，int类型
                domBody[0]["cdefine36"] = ""; //表体自定义项15，DateTime类型
                domBody[0]["cdefine37"] = ""; //表体自定义项16，DateTime类型
                domBody[0]["ioritaxcost"] = ""; //含税单价，double类型
                domBody[0]["icorid"] = ""; //到货单子表id，int类型
                domBody[0]["cbatch"] = ""; //批号，string类型
                domBody[0]["dpdate"] = ""; //生产日期，DateTime类型
                domBody[0]["dvdate"] = ""; //失效日期，DateTime类型
                domBody[0]["imassdate"] = ""; //保质期，int类型
                domBody[0]["bgsp"] = ""; //是否检验，int类型
                domBody[0]["id"] = ""; //主表id，int类型
                domBody[0]["cwhcode"] = ""; //仓库编码，string类型
                domBody[0]["cwhname"] = ""; //仓库名称，string类型
                domBody[0]["cinvname"] = ""; //存货名称，string类型
                domBody[0]["btaxcost"] = ""; //单价标准，string类型
                domBody[0]["binspect"] = ""; //是否已报检，int类型
                domBody[0]["fvalidquantity"] = ""; //合格数量，double类型
                domBody[0]["finvalidquantity"] = ""; //不合格数量，double类型
                domBody[0]["frefusequantity"] = ""; //拒收数量，double类型
                domBody[0]["cinvaddcode"] = ""; //存货代码，string类型
                domBody[0]["frefusenum"] = ""; //拒收件数，double类型
                domBody[0]["cveninvcode"] = ""; //供应商存货编码，string类型
                domBody[0]["cveninvname"] = ""; //供应商存货名称，string类型
                domBody[0]["bexigency"] = ""; //是否急料，int类型
                domBody[0]["cmassunit"] = ""; //保质期单位，int类型
                domBody[0]["ippartid"] = ""; //母件Id，int类型
                domBody[0]["ipquantity"] = ""; //母件数量，int类型
                domBody[0]["iptoseq"] = ""; //选配序号，int类型
                domBody[0]["cordercode"] = ""; //订单号，string类型
                domBody[0]["contractrowno"] = ""; //合同标的编码，string类型
                domBody[0]["contractrowguid"] = ""; //合同标的GUID，string类型
                domBody[0]["contractcode"] = ""; //合同号，string类型
                domBody[0]["sotype"] = ""; //需求跟踪方式，int类型
                domBody[0]["csocode"] = ""; //需求跟踪号，string类型
                domBody[0]["irowno"] = ""; //需求跟踪行号，string类型
                domBody[0]["sodid"] = ""; //需求跟踪子表ID，string类型
                domBody[0]["frealquantity"] = ""; //实收数量，double类型
                domBody[0]["frealnum"] = ""; //实收件数，double类型
                domBody[0]["fsumrefusequantity"] = ""; //已拒收数量，double类型
                domBody[0]["fsumrefusenum"] = ""; //已拒收件数，double类型
                domBody[0]["fvalidnum"] = ""; //合格件数，double类型
                domBody[0]["iexpiratdatecalcu"] = ""; //有效期推算方式，int类型
                domBody[0]["cexpirationdate"] = ""; //有效期至，string类型
                domBody[0]["dexpirationdate"] = ""; //有效期计算项，DateTime类型
                domBody[0]["cbcloser"] = ""; //行关闭人，string类型
                domBody[0]["dValidateDate"] = ""; //有效期，string类型
                domBody[0]["iinvmpcost"] = ""; //最高进价，double类型
                domBody[0]["cupsocode"] = ""; //不良品处理单号，string类型
                domBody[0]["cdemandmemo"] = ""; //需求分类代号说明，string类型
                domBody[0]["iorderdid"] = ""; //销售订单子表id ，int类型
                domBody[0]["iordertype"] = ""; //销售订单类型 ，int类型
                domBody[0]["csoordercode"] = ""; //销售订单号 ，string类型
                domBody[0]["iorderseq"] = ""; //销售订单行号 ，int类型

                //给普通参数VoucherState赋值。此参数的数据类型为int，此参数按值传递，表示单据状态：2新增，1修改，0非编辑
                broker.AssignNormalValue("VoucherState", new int());

                //该参数curID为OUT型参数，由于其数据类型为string，为一般值类型，因此不必传入一个参数变量。在API调用返回时，可以通过GetResult("curID")获取其值

                //该参数CurDom为OUT型参数，由于其数据类型为MSXML2.IXMLDOMDocument2，非一般值类型，因此必须传入一个参数变量。在API调用返回时，可以直接使用该参数
                /*MSXML2.IXMLDOMDocument2 CurDom = new IXMLDOMDocument2();
                broker.AssignNormalValue("CurDom", CurDom);*/
                var curDom = new DOMDocument();
                broker.AssignNormalValue("CurDom", curDom);

                //给普通参数UserMode赋值。此参数的数据类型为int，此参数按值传递，表示模式，0：CS;1:BS
                broker.AssignNormalValue("UserMode", 0);

                //第六步：调用API
                if (!broker.Invoke())
                {
                    //错误处理
                    Exception apiEx = broker.GetException();
                    if (apiEx != null)
                    {
                        if (apiEx is MomSysException)
                        {
                            MomSysException sysEx = apiEx as MomSysException;
                            LogHelper.ErrorLog("【U8API/ArrivedGoods/VoucherSave】系统异常:" + sysEx.Message, apiEx);
                            U8ReturnMessage result = new U8ReturnMessage(0, "系统异常:" + sysEx.Message, "", "0");
                            broker.Release();
                            return result;
                            //Console.WriteLine("系统异常：" + sysEx.Message);
                            //todo:异常处理
                        }
                        else if (apiEx is MomBizException)
                        {
                            MomBizException bizEx = apiEx as MomBizException;
                            LogHelper.ErrorLog("【U8API/ArrivedGoods/VoucherSave(采购订单到货单)】系统异常:" + apiEx.Message, apiEx);
                            U8ReturnMessage result = new U8ReturnMessage(0, "API异常:" + apiEx.Message, "", "0");
                            broker.Release();
                            return result;
                            //Console.WriteLine("API异常：" + bizEx.Message);
                            //todo:异常处理
                        }
                        //异常原因
                        String exReason = broker.GetExceptionString();
                        if (exReason.Length != 0)
                        {
                            LogHelper.ErrorLog("【U8API/ArrivedGoods/VoucherSave(采购订单到货单)】系统异常:" + apiEx.Message, apiEx);
                            U8ReturnMessage result = new U8ReturnMessage(0, "异常原因:" + apiEx.Message, "", "0");
                            broker.Release();
                            return result;
                        }
                    }
                    //结束本次调用，释放API资源
                }

                //第七步：获取返回结果

                //获取返回值
                //获取普通返回值。此返回值数据类型为System.String，此参数按值传递，表示错误描述：空，正确；非空，错误
                System.String resultValue = broker.GetReturnValue() as System.String;
                if (resultValue != "")
                {
                    LogHelper.ErrorLog("【U8API/ArrivedGoods/VoucherSave(采购订单到货单)】调用接口错误:" + resultValue, null);
                    U8ReturnMessage result = new U8ReturnMessage(0, "调用接口错误:" + resultValue, "", "0");
                    broker.Release();
                    return result;
                }
                //获取out/inout参数值

                //获取普通OUT参数curID。此返回值数据类型为string，在使用该参数之前，请判断是否为空
                string curIDRet = broker.GetResult("curID") as string;

                //获取普通OUT参数CurDom。此返回值数据类型为MSXML2.IXMLDOMDocument2，在使用该参数之前，请判断是否为空
                // MSXML2.IXMLDOMDocument2 CurDomRet = Convert.ToObject(broker.GetResult("CurDom"));

                //结束本次调用，释放API资源
                LogHelper.WriteLog("【U8API/ArrivedGoods/VoucherSave(采购订单到货单)】调用成功。");
                U8ReturnMessage successRes = new U8ReturnMessage(1, "调用成功", "", curIDRet);
                broker.Release();
                return successRes;
            }
            catch (Exception ex)
            {
                LogHelper.ErrorLog("【U8API/ArrivedGoods/VoucherSave(采购订单到货单)】Exception:" + ex.Message, ex);
                U8ReturnMessage ExceptionRes = new U8ReturnMessage(0, "Exception:" + ex.Message, "", "0");
                return ExceptionRes;
            }
        }

    }

}