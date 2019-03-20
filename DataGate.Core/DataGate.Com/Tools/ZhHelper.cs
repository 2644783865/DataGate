﻿using System;
using System.Text;

namespace DataGate.Com
{

    /// <summary>
    /// 全、简拼处理类
    /// </summary>
    public static class ZhHelper
    {
        static ZhHelper()
        {
            //https://www.cnblogs.com/chr-wonder/p/8464204.html
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        }

        #region GB2312中的汉字编码
        /// <summary>
        /// 01-09区为特殊符号。 
        /// 16-55区为一级汉字，按拼音排序。 
        /// 56-87区为二级汉字，按部首/笔画排序。
        /// 每个汉字及符号以两个字节来表示。第一个字节称为“高位字节”，第二个字节称为“低位字节”。
        /// “高位字节”使用了0xA1-0xF7(把01-87区的区号加上0xA0)，“低位字节”使用了0xA1-0xFE(把01-94加上0xA0)。
        /// 例如“啊”字在大多数程序中，会以0xB0A1储存。（与区位码对比：0xB0=0xA0+16,0xA1=0xA0+1）。
        /// </summary>
        private static readonly string[][] _Allhz = new string[][]
        {
            new string[]{"a","啊锕嗄阿吖"},
            new string[]{"aes","厑"},
            new string[]{"ai","昹躷譪伌硋壒溰懓鴱塧皧嘊鑀凒啀溾隘嗌嫒敳嗳哎哀唉埃挨欸锿捱瞹癌叆矮蔼霭艾爱砹鱫暧馤皑瑷僾薆餲碍"},
            new string[]{"an","罯犴岸按案胺黯錌貋婩堓峖荌铵隌雸韽馣誝蓭萻豻闇安桉氨庵谙媕盦腤晻鞍侒鮟盫啽垵俺唵埯暗揞鹌"},
            new string[]{"ang","岇昻肮昂枊盎醠"},
            new string[]{"ao","翱鳌謷鏖聱岙璈熬螯芺獒媪廒傲奡奥骜墺澳懊鏊袄岰厫隞嗸滶獓蔜翶遨扷嫯慠敖鷔嗷奧爊垇坳凹謸擙嶅"},
            new string[]{"ba","胈八吧巴叭扒朳叐灞欛矲軷釛癹炦拔抜罢魞釟蚆羓紦哵玐仈坺魃岜芭疤捌粑鲃茇笆霸跋鲅鼥把钯靶坝弝爸夿菝豝"},
            new string[]{"bai","柏摆贁薭拝粨捭挀栢佰百白掰败拜稗竡"},
            new string[]{"baike","兡"},
            new string[]{"baiwa","瓸"},
            new string[]{"ban","瘢伴拌姅扳攽班般颁斑搬斒昄半办魬蝂舨钣版板螁瓣岅阪瓪粄怑秚鉡靽癍坂扮湴辬绊"},
            new string[]{"bang","邦蒡稖塝谤棓棒梆傍镑磅帮蚌幚幇膀榜浜绑艕玤峀鞤垹縍捠邫"},
            new string[]{"bao","靤勽靌趵鳵鲍駂褒胞菢报賲抱骲寶髱儤曓孢剥勹龅煲爆苞虣包忁珤雹宝饱保堡暴瀑飹寚豹堢鸨嫑窇闁葆蕔笣佨褓薄媬宀"},
            new string[]{"bei","备俻苝牬昁鉳喺藣钡揹梖背珼邶狈陂卑杯悲碑鹎垻北孛偝蓓褙骳辈糒鞴誖鐾琲偹惫碚贝被悖呗蛽禙愂軰僃倍鄁焙"},
            new string[]{"ben","渀贲撪桳夯捹楍奙锛奔輽倴本苯畚坌笨犇"},
            new string[]{"beng","菶塴嘣泵絣绷迸甏镚甭鞛琫崩祊伻嵭閍蹦"},
            new string[]{"bi","妼怭袐蓖坒匂毴嬶朼疕柀粊佖聛萞避鼊閟弊碧箅蔽馝駜髲壁嬖篦裨觱鵖鮅濞臂鞞髀奰璧饆襞襣躄鷩薜罼弻湢貱彃楅滭蜌飶熚獙綼鄪跸獘閇襅廦縪鄨繴鏎韠躃魓驆鷝腷幤邲毙鄙必毕疪闭币诐夶畀苾皕毖荜陛庇柲沘妣吡佊比匕痹荸珌鎞鲾逼偪屄皀鼻堛彼秕哔狴舭俾笔鞁佛梐敝铋婢庳滗煏弼筚赑愎萆愊"},
            new string[]{"bia","髟"},
            new string[]{"bian","便变揙缏遍艑昪辨辩藊辫卞弁忭汳苄汴碥鴘抃鞭峅笾编萹煸箯砭鳊边贬扁豍窆匾惼蝙炞稨辧甂邉鍽辡鯾褊変釆"},
            new string[]{"biao","婊裱表镳镖飙飚瀌瘭爂熛鳔骠俵脿墂猋颷褾贆飊諘滮儦彪标飑幖膘檦"},
            new string[]{"bie","虌憋鳖别襒瘪蹩龞別咇莂蛂徶"},
            new string[]{"bin","滨彬宾傧斌蠙缤濒镔槟豳邠玢殡摈膑髌鬓椕濵虨霦顮氞鬂"},
            new string[]{"bing","炳柄秉垪饼怲倂鞆鈵棅窉昺燷抦併陃鋲梹仌兵冰苪栟蛃禀并幷病摒餠丙疒靐鮩誁傡栤庰邴"},
            new string[]{"bo","鹁袯襮镈踣膊箔鲌跛亳簸葧渤博舶馎铂钹浡搏伯波玻钵饽啵脖菠礴播拨驳帛泊瓝勃卜檗擘嶓蘗妭癶盋僠蹳驋鱍仢狛侼胉郣挬秡孹僰愽譒糪箥欂犦簙懪鋍馛艊礡煿牔"},
            new string[]{"bu","餔鳪捕哺补卟歩钸庯晡不蔀逋醭布鸔步怖钚部瓿篰簿咘峬轐歨埠埗悑廍誧餢勏踄佈"},
            new string[]{"ca","礸遪礤擦"},
            new string[]{"cai","材彩采睬才縩财猜綵踩裁蔡寀棌婇埰戝菜"},
            new string[]{"cal","乲"},
            new string[]{"can","粲灿黪璨薒澯儏噆蠺蝅嬠湌飡蚕憯爘残餐骖参惨惭"},
            new string[]{"cang","濸賶欌螥獊嵢凔鑶罉舱苍沧伧仓藏"},
            new string[]{"cao","螬草艚蓸曺褿鏪艹愺騲肏襙嶆漕嘈曹槽糙操"},
            new string[]{"ce","墄荝册萗刂蓛憡侧厕箣恻策测敇萴"},
            new string[]{"cen","梣膥嵾岑涔"},
            new string[]{"ceng","竲曾层驓噌蹭"},
            new string[]{"ceok","硛硳"},
            new string[]{"ceom","岾"},
            new string[]{"ceon","猠"},
            new string[]{"ceor","乽"},
            new string[]{"cha","刹芆偛嗏銟疀奼肞锸臿猹嚓馇槎插叉杈垞姹查诧茬茶嵖搽岔碴汊艖察檫差衩镲侘"},
            new string[]{"chai","犲祡拆钗侪柴豺虿囆喍袃"},
            new string[]{"chan","蝉鋋廛镡梴潺缠馋瀍禅刬孱谗婵辿谄韂羼颤忏繟冁镵阐蟾湹浐澶产鋓躔巉蒇摲幝簅搀灛讇幨铲嵼懴鄽毚磛劖棎硟嚵煘獑觇酁誗閳緾剷壥瀺掺纒艬旵丳潹"},
            new string[]{"chang","娼伥昌场嚐韔唱鬯倡畅玚怅鋹氅敞鲳昶猖厂鲿嫦徜偿鋿尝苌肠阊菖惝畼淐常誯厰僘瑺鏛蟐椙晿瓺琩裮鼚兏锠"},
            new string[]{"chao","耖欩怊钞焯超晁巢吵嘲绰眧麨弨潮朝仦抄炒觘巐煼轈罺窲漅鄛巣牊"},
            new string[]{"che","扯掣硩勶瞮烢澈撤頙偖彻聅唓砗车迠爡莗坼蛼"},
            new string[]{"chen","碜墋踸衬疢龀榇趻谶趁谌晨宸陈辰沉忱臣尘嗔琛抻茞郴螴嚫齓贂夦鷐曟薼霃瘎樄蔯煁諃敐莐烥麎賝揨醦"},
            new string[]{"cheng","筬窚掁酲珹騬挰荿洆裎塖呈碀悜瀓鯎峸棦檙畻庱浾惩秤骋逞橙澄塍阷憆溗郕睈摚靗緽頳竀鏳鏿饓泟脭称铖丞瞠撑赪琤承成铛程絾枨牚诚蛏柽城宬乘埕偁"},
            new string[]{"chi","瘛赤啻憏吃哧蚩鸱傺墀笞嗤媸摛痴螭魑弛池驰迟眵持瓻踟篪尺侈齿耻豉褫彳叱斥饬茌妛垑卶肔叺謘抶遅貾筂翅荎歯杘侙彨訵瞝鵄齝麶彲黐淔鉓炽岻鷘饎趩懘翨慗遫鶒胣雴蚇腟痸湁痓烾恜灻鉹裭敕翤"},
            new string[]{"chong","充宠崇虫艟憃翀茺铳冲憧浺珫嘃摏罿蹖崈隀忡舂沖"},
            new string[]{"chou","皗臭殠犫栦丑絒雠雔嬦燽丒吜杽侴菗遚抽篘犨仇俦帱瞅惆瘳绸畴愁筹酬踌懤稠"},
            new string[]{"chu","蒭出椘蟵櫉篨濋趎檚榋豠蒢貙岀臅犓珿斶橻儊閦鄐禇傗初豖拀竌処齭璴琡触屮齼亍处怵楚搐储憷歜黜矗鶵畜绌蜍摴樗刍除厨褚锄础雏橱幮躇蹰杵滁楮"},
            new string[]{"chuai","膪搋膗揣嘬踹"},
            new string[]{"chuan","穿玔汌輲暷瑏剶巛賗氚歂传舡船遄椽舛荈喘串钏川"},
            new string[]{"chuang","窗剙剏刱磢傸窻牎刅闯疮摐噇创怆床仺"},
            new string[]{"chui","捶顀桘龡锤槌菙陲炊吹棰垂"},
            new string[]{"chun","槆瑃箺橁櫄賰陙杶犉滣鯙偆萶睶鶞纯蠢踳醇漘鹑暙莼媋蝽椿堾春僢鰆淳唇"},
            new string[]{"chuo","逴歠辍辵戳踔啜涰酫擉磭嚽鑡龊"},
            new string[]{"ci","礠偨珁垐柌飺嬨螆鴜疵皉朿刾茦栨莿絘蛓濨磁赐刺佽次佌此糍趀雌呲粢辞慈庛瓷茨祠词鹚茈"},
            new string[]{"cis","嗭"},
            new string[]{"cong","苁暰漗忩囪琮匆聦枞葱骢璁聪丛淙从孮爜欉灇藂賩賨誴樬徖瑽婃騘繱蟌篵瞛焧悰聡"},
            new string[]{"cou","玼凑辏腠"},
            new string[]{"cu","縬憱趗誎瘄顣娖鼀踀麄麁媨蔟粗徂殂瘯猝醋簇蹙蹴促"},
            new string[]{"cuan","蹿爨篡窜镩撺汆殩巑櫕"},
            new string[]{"cui","悴摧榱璀催啐凗淬萃毳瘁粹翠膬脆乼獕磪崔竁焠皠臎慛膵墔鏙"},
            new string[]{"cun","竴村籿刌忖皴寸存"},
            new string[]{"cuo","剒遳虘嵳蒫襊剉瑳夎莝莡逪棤蓌齹蹉瘥搓醝撮嵯痤矬鹾脞厝挫措锉错磋"},
            new string[]{"da","鐽羍荙匒詚跶燵蟽墶龖龘亣眔橽繨妲打鞑靼瘩答笪炟耷垯达褡哒嗒咑大怛搭"},
            new string[]{"dai","迨呆歹傣代岱埭绐带待怠殆玳贷甙叇襶霴瀻艜鴏鮘懛曃轪軩帯柋帒岱汏緿逮戴黛袋侢"},
            new string[]{"dan","褝澸衴瓭玬狚刐聸勯沊赕匰媅躭襌蜑鴠駳禫憺髧黕觛帎腅啿疍柦泹霮窞瘅妉担紞疸胆甔箪襜殚聃郸眈单丹儋弹澹饏氮蛋萏亻惮砃啖诞但旦掸卩淡癚耽贉嚪"},
            new string[]{"dang","澢噹儅挡筜珰凼蟷宕砀荡档菪谠艡党当礑璗譡灙圵垱瓽雼壋瞊趤裆"},
            new string[]{"dao","嶹釖檤衜道嶋噵槝稲盜陦壔衟菿裯椡舠隝屶鱽軇瓙禂隯捣叨忉氘稻导刀倒纛翿祷蹈到悼盗岛嶌"},
            new string[]{"de","淂徳恴得德的锝"},
            new string[]{"dem","揼"},
            new string[]{"den","扥扽"},
            new string[]{"deng","登隥竳簦墱等戥嬁艠蹬豋邓镫嶝瞪灯璒磴噔覴凳"},
            new string[]{"di","籴砥睇谛骶地弟帝娣递第柢嚁玓堤鸐蔐蔋靮梑敌苖羝缔嘀迪滴镝鞮狄低荻底坻邸诋氐嫡頔笛抵弤涤呧阺蒂豴觌墬焍眱僀慸嶳碲棣彽坘祶赿螮禘墑馰腣鉪菂趆仾廸菧軧甋磾梊唙旳袛啲偙鯳逓怟埊俤埞"},
            new string[]{"dia","嗲"},
            new string[]{"dian","碘掂蹎厧嵮槙瘨丶敁靛婝典癫巅颠滇驔磹橂壂扂琔巔踮蕇蒧椣敟婰奌齻攧蜔癜点电傎簟殿奠淀惦阽玷垫店坫佃钿甸"},
            new string[]{"diao","雿竨鈟訋伄汈鼦鮉奝簓弴瘹魡蛁琱铫鲷鑃窎屌刁叼凋貂钓雕刟吊殦掉铞鋽窵碉"},
            new string[]{"die","镻峌恎绖胅眣耊戜氎疂苵褋牃詄畳艓堞跮爹跌迭垤瓞蝶谍褺揲耋叠牒碟蹀鲽幉"},
            new string[]{"dim","嚸"},
            new string[]{"ding","玎顶帄靪嵿町铤锭碇腚啶定仃鼎鐤酊顁椗萣订饤耵碠磸薡丁叮疔盯钉聢"},
            new string[]{"diu","丢铥丟"},
            new string[]{"dong","岽咚冬东恫駧董揰硐鸫胴胨崠栋働峒垌侗冻动蕫懂洞鶫苳昸埬崬涷笗菄氭蝀霘鯟戙嬞箽諌姛挏迵鮗夂"},
            new string[]{"dou","兜蔸篼抖斗脰閗剅唗橷艔浢荳饾斣酘窦都鬪鬬陡蚪豆郖逗痘梪"},
            new string[]{"du","髑笃蠹镀渡度肚杜芏犊堵嘟独黩牍椟渎读毒督赌豄殬螙靯秺荰琽帾厾讟韥韇鑟醏贕涜騳皾瓄殰嬻匵凟錖蝳読裻妒韣睹"},
            new string[]{"duan","葮断缎椴躖毈碫篅瑖塅鍴媏偳煅短簖端段锻腶"},
            new string[]{"dug","叾"},
            new string[]{"dui","対陮綐薱嵟瀩譈譵濧頧垖憝碓怼祋兑痽对队堆鴭塠"},
            new string[]{"dun","炖踲趸犜撉獤伅盾鐜沌碷潡礅蹲惇驐撴逇敦盹砘壿镦蹾吨墩遁顿钝"},
            new string[]{"duo","嚲沲凙堕亸剫嚉鮵挅挆埵鈬剁舵趓毲痥多惰跺鵽錞朵踱敚掇铎夺裰桗咄哚嶞鬌墯崜尮垛饳剟陊躲哆"},
            new string[]{"e","堮崿枙妿娿囮迗涐珴皒睋鈋磀咢頟硆砈騀鵈歺砐峉匎砨蚅偔卾誐颚婀厄阨呃扼苊轭垩湂恶饿噩鹅鹗锷腭遏萼愕砵阏鄂谔僫擜豟軶遌廅搹妸琧屙讹俄额娥蛾峨覨餩歞莪櫮讍鑩锇鳄蝁"},
            new string[]{"ei","诶"},
            new string[]{"en","峎摁唔奀恩蒽"},
            new string[]{"eng","鞥"},
            new string[]{"eo","仒"},
            new string[]{"eol","乻"},
            new string[]{"eos","旕"},
            new string[]{"er","尔鲕鸸而珥儿佴饵洱迩耳毦峏洏栭袻粫輀隭髵侕栮荋駬趰弍弐刵咡貮衈誀轜贰铒胹二"},
            new string[]{"fa","疺姂醗伐傠发垡彂発沷罚髪筏藅佱鍅阀乏砝瞂珐法"},
            new string[]{"fan","反橎氾舧舤笲返畈贩范犯汎憣饭泛梵棥帆番幡翻藩凡矾钒烦颿轓樊蹯燔籵凢匥襎凣鱕軓璠繁杋柉蘩薠蕃辺盕瀿鐇墦瀪蠜鷭勫羳払旛奿旙訉滼嬏忛攵飰犭笵仮"},
            new string[]{"fang","淓鲂房鴋堏昞鶭髣旊眆瓬防埅牥錺方邡坊芳枋肪钫昘访纺妨昉舫放仿"},
            new string[]{"fei","馡昲杮蕜篚蟦暃飝俷騑厞餥騛费裶吠婔屝棐廃曊癈废鼣沸濷櫠剕鲱狒墢肺飞靅妃非啡痱绯菲扉猆霏翡肥淝腓匪诽悱镄斐榧婓渄靟蜚"},
            new string[]{"fen","黺羵鲼瀵棻秎愤坋偾瞓黂馩鐼轒鼖帉棼坟酚氛芬纷粪分弅焚鼢粉份奋忿吩訜肦炃枌岎膹妢燓汾豮饙餴朆梤躮蚡燌橨鳻魵幩隫鈖蒶蚠羒昐竕翂馚"},
            new string[]{"feng","疯酆砜葑冯逢缝讽唪凤奉枫锋丰烽峰飌封沨沣风俸崶寷捀鎽檒鄷篈蜂僼甮猦靊琒桻偑盽凮妦凬仹埄犎鳯鏠麷蘕鴌煈焨湗艂綘堸赗"},
            new string[]{"fenwa","瓰"},
            new string[]{"fiao","覅"},
            new string[]{"fo","坲梻仏"},
            new string[]{"fou","缶紑缹缻雬鴀否"},
            new string[]{"fu","褔鉘箙罦蜉簠緮蕧輹鍑諨蚹澓鈇稪榑鉜鍢艀綍葍絥鴔捬颫冨偩袝蚥祔竎盙韨椨芣釡郙弣鶝鵩鮲癁鮄琈焤玞虙筟酜旉荴娐衭荂糐怤麬枎姇邞伕柭嬔嬎韛颰砆彿媍畗栿哹畉玸炥柫綒泭翇帗岪咈刜冹甶乀懯麱垘桴氟祓罘茯郛浮砩莩婏匐苻涪符艴菔袱幅福辐蚨凫呋肤趺麸稃跗孵敷枹伏俘孚扶芙怫拂服绂绋黻弗蝜幞赴副傅富赋缚腹驸赙阜蝮鳆覆馥禣椱詂覄蛗鲋滏夫抚甫府拊斧俯釜复腑蝠腐黼父讣付妇负附咐辅"},
            new string[]{"fui","猤"},
            new string[]{"ga","尬玍旮呷嘠錷尕噶嘎尜钆伽魀"},
            new string[]{"gai","忋峐姟晐畡祴賌絠鎅乢匄豥盖该陔垓赅改槪钙荄溉戤概葢漑瓂丐"},
            new string[]{"gan","皯玵灨檊詌凎倝盰鳡簳玕衦骭魐尶漧筸尲凲亁粓迀芉笴秆甘杆肝坩泔苷柑竿疳干尴矸赶敢感澉橄擀旰绀幹赣酐淦"},
            new string[]{"gang","肛鎠罁犅堈牨冮罓冈刚焵纲罡缸钢港筻戆掆棡堽岗杠"},
            new string[]{"gao","高锆鷱鼛鷎獔韟櫜檺髙告禞筶祰祮峼勂吿槹夰羔皋睾膏篙糕槔郜菒诰杲搞缟槁稿镐藁稁"},
            new string[]{"ge","呄牱滒戨鴐鎶革佮匌愅臵鴚鸽滆咯杚戈圪纥疙哥鬲袼隔割搁歌阁彁格觡葛胳个箇铬硌戓各韐舸哿骼镉膈搿塥嗝諽鰪騔鞷轕韚鮯虼櫊"},
            new string[]{"gei","给"},
            new string[]{"gen","根搄跟亘艮茛揯哏"},
            new string[]{"geng","羹挭菮莄絚焿椩耕庚更峺赓堩郠刯浭掶哽埂绠耿梗鲠鹒"},
            new string[]{"geu","啹"},
            new string[]{"gib","喼"},
            new string[]{"go","嗰"},
            new string[]{"gong","龚攻贡共珙拱栱汞巩塨觥糼髸拲輁熕匔幊肱宮躬杛弓公功碽供宫恭蚣工匑"},
            new string[]{"gongfen","兝"},
            new string[]{"gongli","兣"},
            new string[]{"gou","够勾耉耈诟袧垢褠媾彀遘簼笱耇枸撀煹雊訽夠茩姤坸豿玽苟狗沟钩缑佝篝购觏鞲构岣"},
            new string[]{"gu","薣骨凅嘏濲榖鼔硲鼓愲盬篐蛌蛊脵啒傦逧峠唂橭柧嫴箛軱笟罛辜唃牯鮕瀔毂锢鲴孤姑咕估钴罟雇堌牿榾馉羖蓇棝祻稒頋尳崓臌谷穀诂汩古鹘箍酤觚蛄痼菇股鸪轱沽瞽固故顾崮梏菰"},
            new string[]{"gua","騧颪罣坬叧瓜歄焻煱劀銽冎緺卦呱啩剐鸹寡诖挂褂胍刮"},
            new string[]{"guai","乖拐罫怪"},
            new string[]{"guan","冠盥棺鳏馆管倌贯惯涫灌蒄樌瓘鹳罐掼悹关观官鱹輨礶爟祼遦窤悺泴鳤錧筦痯鱞癏罆瘝躀莞"},
            new string[]{"guang","広光咣桄胱广犷撗欟黆銧炚灮逛炗輄炛垙姯茪烡珖僙侊"},
            new string[]{"gui","规轨宄郌桧瑰皈妫龟炅庋闺柜帰亀袿媯鳜跪桂刿硅匦圭椝刽簋晷鬼癸诡贵攰归鲑鱥鳺禬槶撌瞆蓕筀邽攱蟡櫷摫槻璝瞡椢鬶厬陒祪湀蛫觤"},
            new string[]{"gun","蔉鲧謴睴棞棍惃磙滚辊绲衮璭蓘"},
            new string[]{"guo","椁蔮果馘虢掴帼蜾囯褁蝈锅聒崞郭国囻鈛墎瘑嘓彉簂囶裹慖漍聝惈綶过彍圀啯馃菓埚呙"},
            new string[]{"ha","铪哈蛤丷"},
            new string[]{"hai","嗐氦酼侅烸塰郂餀骇饚嚡亥醢害胲海骸孩駴"},
            new string[]{"hal","乤"},
            new string[]{"han","鼾翰瀚晗浛函邯含嵅忓邗撖筨憨酣爳蚶蜬鋎甝悍凾焓寒韩罕喊汉汗撼娢憾崡梒捍焊琀菡傼颔澏旱蛿顸涆莟汵晥皔垾睅晘蜭暵鶾雗駻顄螒仠佄鋡厈浫馠谽哻魽兯涵蔊闬豃鬫屽"},
            new string[]{"hang","蚢沆行颃航魧苀筕笐绗貥杭垳"},
            new string[]{"hao","皡澔皓暤郝皞灏鰝皥颢聕淏耗浩薃号蒿好濠壕嚎豪嗥毫蚝薅嚆昊獆哠暭晧傐昦獋恏嘷椃竓譹儫曍籇"},
            new string[]{"haoke","兞"},
            new string[]{"he","詥姀盍荷阂曷河核和鉌涸熆禾嗬喝呵诃合峆啝翯渮萂盉敆蠚哬籺龁惒粭菏柇盒壑鹤赫褐贺佫翮阖颌何訸犵楁湼靏鸖靎齃劾爀癋鹖煂靍麧寉垎鑉澕篕皬碋"},
            new string[]{"hei","嘿黑黒嗨潶嬒"},
            new string[]{"hen","恨狠痕拫鞎詪很"},
            new string[]{"heng","脝哼烆亨恒横姮鸻蘅桁鵆涥珩悙胻衡鑅堼"},
            new string[]{"heui","囍"},
            new string[]{"hol","乥"},
            new string[]{"hong","鋐澋彋篊宏汯红霐闳纮玒虹荭玜苰嗊渱霟弘撔娂晎浤洪垬耾焢鸿軣灴叿輷黉嚝鍧仜宖妅吰粠鈜鉷潂谼翝泓紭葓硔竤硡谹蕻翃鞃綋羾薨烘轰竑哄訇讧"},
            new string[]{"hou","吼堠候逅厚鲎后骺糇篌侯瘊郈鄇喉犼垕後葔鲘洉睺翭鍭鯸猴豞帿"},
            new string[]{"hu","岵焀瓳喖媩壷絗狐唿虖轷楜胡呼熩忽虍雽匫苸泘曶昒垀嘝匢寣烀昈枑粐婟綔嫭嫮乕槴弖乎頀韄鳠鍙簄鳸蔰鶦箶魱螜頶戽鍸餬冴鰗戸淈萀雐虝箎錿鯱蔛瀫唬俿鹄斛囫怙糊觳歑浒滹琥互户冱护沪惚弧虎縎淴縠槲鹕壶蝴瑚醐笏煳猢葫鹱鄠怘瓠扈湖祜"},
            new string[]{"hua","砉畵劃摦槬嫿澅黊繣吪魤錵婳崋杹鷨华粿嬅撶螖誮糀硴椛婲埖芲花话铧滑猾化璍画骅桦哗划"},
            new string[]{"huai","蘹壊蘾耲褱褢坏怀徊淮槐踝懐"},
            new string[]{"huan","鯶歓鴅圜睆讙驩唤环洹桓萑锾寰缳鬟缓幻瞣宦欢换浣涣患焕擐鲩逭漶痪奂烉萈堚寏雈羦貆阛豲鹮糫还肒獾梙喚嵈愌瑍槵藧豢鰀犿荁攌"},
            new string[]{"huang","湟谎晃恍鳇蟥簧磺癀蝗篁璜潢趪遑鎤惶徨黄隍凰皇慌荒肓鐄锽喤塃煌鱑衁偟堭媓崲葟楻瑝墴獚艎穔諻幌詤巟皝曂榥兤餭縨騜奛晄宺炾鷬滉皩"},
            new string[]{"hui","晖喙惠缋毁慧蕙蟪徽麾瞺珲彗虺挥灰恢咴诙顪回鐬櫘辉槥洄茴蛔悔卉汇会讳哕襘秽蔧晦浍绘荟诲恚靧隳烩贿譓鞼噅檓毇螝鮰痐逥恛廽囬囘鰴譿噕芔睳幑禈翚媈婎豗烣洃拻灳蘳瀈嚖薉檅獩橞徻屷餯泋璤憓潓寭圚嘒詯彚阓翙恵懳暳"},
            new string[]{"hun","忶诨混溷馄浑魂倱涽惽棔睧睯鼲俒阍掍焝慁觨婚荤昏繉"},
            new string[]{"huo","耯湱曤矐癨臛嚿姡瀖謋騞嗀腘剨眓俰咟沎邩秮佸吙奯货锪劐豁攉活火伙趏钬或耠获豰祸惑霍镬嚯藿蠖夥"},
            new string[]{"hwa","夻"},
            new string[]{"ji","芶畿鰿屐积笄基绩嵇犄缉赍畸唧箕剞稽齑墼激羁及吉岌骥髻冀鲫跻击檵穄罽刉垍芰蹐鹡伋丌茤姬岋霁叽饥乩圾机玑肌芨矶鸡咭迹讥汲脊挤虮己几籍蕺瘠辑蒺楫稷集嵴殛棘戢疾笈急姞佶亟极即级嫉哜鰶跽暨蓟祭悸寄寂偈觊继济掎既戟季剂际技忌妓纪伎记计麂鲚洎愱鏶亼彶忣郆卽叝皍揤觙偮谻鸄塉鑙槉膌銡嶯潗箿蕀橶螏襋鍓艥湒錤刏枅喞嵆筓勣嗘鳮僟銈槣樭覊藉霵隮簊賷櫅耭譤癪鞿羇虀鑇覉躸襀蘻蠀轚簎畟諔漃禝稩穊誋裚諅墍檕鵋齌廭懻癠糭鯚瀱蘮鱀暩彑鷑雦雧丮妀犱泲鱾幾堲穖継旡梞痵兾葪臮撠惎坖旣徛剤茍峜"},
            new string[]{"jia","饸挟耞假胛豭圿颊价驾榢幏榎婽斚玾鵊鴶钾扴贾麚貑糘鉫腵犌猳梜毠泇抸筴頬跏稼架加夹佳迦枷浃珈家痂笳瘕葭戛甲槚蛱袈铗嫁恝荚郏岬镓嘉"},
            new string[]{"jialun","嗧"},
            new string[]{"jian","笺煎鐗徤剱珔剣俴洊兼襺臶蠒鹸鰎鬋謭襉鹼劒繝擶礀磵瞷薦寋劔旔趝諓糋墹劎賎絸橺睷藆馢熸蕳蕑緘瀐樫殱葏葌菺猏惤姧冿熞虃鑳瑐弿詃検湕麉倹彅囏韀籛瀸鵳覸礛挸翦牮睑裥锏简谫检碱剪謇蹇见件建饯轞戬拣瑊硷缣蒹鲣鹣趼囝荐枧俭柬茧捡笕减鞯艰剑瀽尖幵坚歼搛肩戋监菅湔犍缄牋间溅贱健涧舰渐谏奸毽鑬腱践鉴键僭箭踺楗"},
            new string[]{"jiang","僵奖糨犟酱绛降匠耩浆桨江讲疆礓缰豇茳将姜蒋奬櫤醤嵹滰摾杢膙夅謽顜袶傋疅鳉橿壃螀翞葁畕匞弶洚弜"},
            new string[]{"jiao","骄鹪礁蕉鲛僬跤蛟焦佼胶挢茭浇娇姣郊交艽椒敫憿醮噍酵窖教较轿叫角徼嘂剿搅铰脚矫皎饺绞狡缴踋穚簥蟭鐎鷮櫵臫晈煍峧暞嶣劋敽敿曒璬皦鵤孂纐賋趭灚嘦斠漖藠嚼觉釂皭膲譥憍獥嬓珓訆挍鱎芁虠嶕滘"},
            new string[]{"jie","蓵琾丯岕庎忦畍砎衸徣届楐蠽魪骱蚧借鉣诫疥昅堺誱衱迼桝莭崨嵥楶滐飷榤媎踕幯嶻擮礍鍻蠞蠘芥蜐痎孑街揭嗟喈秸接皆界阶劫劼袺毑檞屆玠犗褯疖颉戒介解姐羯鲒竭碣节睫讦捷婕桀结洁拮诘杰蛶截媘岊紒趌蝍跲掲堦曁脻煯鞂蝔擑尐刦刧妎"},
            new string[]{"jin","凚嬧壗嚍溍煡枃侭璶漌晋巹黅蓳齽巾祲鹶荩瑨浸烬赆缙禁靳觐噤钅谨金紧斤馑矜衿琎瑾锦槿堇筋琻襟今仅卺廑兓堻惍珒觔津釒搢进劲砛尽妗近殣"},
            new string[]{"jing","颈晶粳睛腈精鲸井阱刭肼兢镜猄麠菁璟迳逕獍境靖婧竟景静竞痉胫径弪净警憬儆敬聙憼汬幜穽汫鼱麖鶁旌橸暻稉経旍婛秔亰坕劤鵛傹惊荆茎经泾京竸瀞璄誩燝竫梷桱婙浄妌蟼璥曔"},
            new string[]{"jiong","窘埛蘏褧燛煛僒綗颎蘔侰澃駉冋扃迥坰駫炯浻絅冏囧煚"},
            new string[]{"jiu","疚酒鸠纠究韭赳殧久臼灸齨阄鬏柩桕厩救就舅僦鹫揪咎柾慦廐廏玖倃匛镹紤啾舏杦汣奺旧匓乣麔鯦九萛勼朻糺丩乆匶"},
            new string[]{"ju","裾雎鞠鞫桔锔狙菊局琚椐掬疽驹苴拘居屦橘蒟犋飓锯聚踞遽醵窭澽惧筥鶪湨焗侷腒泃沮拒举莒榉龃踽句巨距姖据苣具炬钜俱倨剧咀讵椈踘鴡鶋泦狊毩駶郹艍毱犑輂粷躹閰檋淗陱爠躆簴壉凥匊倶諊罝蜛娵婅婮崌梮涺伡挶豦歫秬粔鵙蚷埧昛虡冣懅愳駏鮔矩勮埾椇蹫鵴巈蘜驧耟挙拠聥怇邭襷岠洰欅"},
            new string[]{"juan","卷羂劵姢裐餋菤呟埍奆桊臇淃睊勬罥鹃涓镌蠲捐娟眷鄄慻绢锩倦狷"},
            new string[]{"jue","爴镢刔爵倔谲劂厥觖桷蕨掘孒崛亅绝珏玦抉撧屫獗橛瑴攫蹶噘撅孓决臄镼憠瘚憰嶥氒鈌趉覚殌诀欮虳砄挗芵熦屩貜龣矡欔玃彏叏灍鷢鴃钁蟨爑蟩爝矍觼戄"},
            new string[]{"jun","寯碅莙覠隽畯均皹鵘鵕鵔皲懏钧蜠箟晙埈陖君军呁銁鍕銞燇汮珺竣蔨桾蚐馂袀菌骏浚麇筠捃峻郡鲪俊"},
            new string[]{"ka","胩咔咖喀卡佧鉲垰裃"},
            new string[]{"kai","开慨闿凯剀锎揩炌恺垲铠鎎蒈烗颽楷锴忾暟奒噄勓"},
            new string[]{"kal","乫"},
            new string[]{"kan","看槛栞冚侃譼磡墈崁衎刊偘勘龛堪瞰坎塪矙莰阚惂輡砍竷戡轗"},
            new string[]{"kang","邟匟鏮嫝嵻漮躿扛砊糠槺伉慷钪鱇亢囥抗闶炕康"},
            new string[]{"kao","栲鯌髛攷鲓考靠犒拷尻铐薧烤"},
            new string[]{"ke","可岢咳嵙壳恪渴克刻客课氪髁趷窠痾萪犐薖樖醘胢科欬磆牁榼悈濭坷苛瞌珂衉轲疴钶棵颏稞蝌颗礚柯勊锞碦磕缂堁嗑娔翗溘勀敤骒渇炣嶱尅"},
            new string[]{"ken","錹肯肻啃垦恳裉掯"},
            new string[]{"keng","劥硁牼硻誙坑铿吭銵鍞"},
            new string[]{"keo","巪"},
            new string[]{"keol","乬"},
            new string[]{"keos","唟"},
            new string[]{"keum","厼"},
            new string[]{"ki","怾"},
            new string[]{"kong","埪孔悾硿恐躻錓鵼控箜崆空鞚倥"},
            new string[]{"kos","廤"},
            new string[]{"kou","簆蔻怐口宼鷇寇冦扣瞉蔲叩眍筘劶彄剾芤抠滱"},
            new string[]{"ku","郀桍崫跍秙刳枯哭堀窟袴库苦喾裤酷狜齁骷捁绔圐俈瘔"},
            new string[]{"kua","垮挎跨胯夸骻蕐銙舿姱侉"},
            new string[]{"kuai","哙块郐蒯狯脍筷侩巜快鲙擓凷廥糩旝圦"},
            new string[]{"kuan","髋寛宽款窾"},
            new string[]{"kuang","圹匡矿眶况纩框邝夼诳狂筐诓贶劻洭哐眖匩恇筺狅軖軠鵟儣懭穬昿邼砿絋絖軦鉱鋛黋懬矌岲丱旷硄卝"},
            new string[]{"kui","晆顝鄈暌頄跬篑馈蒉匮戣睽聩奎蝰夔傀岿悝楏溃窥魁逵馗喹揆葵亏盔聧嘳嬇聭樻籄媿楑鐀刲骙喟殨愦愧鑎櫆蹞頍煃卼躨頯虁蘷鍷藈鍨欳"},
            new string[]{"kun","坤梱壸鹍锟裈昆阃琨髡醌鲲悃捆涃豤焜尡裍困晜堒婫崐崑猑菎潉裩睏蜫熴瑻臗騉祵硱稇稛髠閸"},
            new string[]{"kuo","阔懖韕鞟鞹筈鬠扩括霩蛞廓頢拡秳挄桰葀栝"},
            new string[]{"kweok","穒"},
            new string[]{"la","瓎鞡镴楋蝋蝲鬎鯻柆揧溂嚹藞菈翋臈腊垃拉啦旯砬剌邋瘌蜡辣喇"},
            new string[]{"lai","麳鶆俫騋箂唻頼筙藾鯠襰崃来瀬庲莱籁铼琜癞濑櫴徕涞梾赉猍赖逨睐棶攋"},
            new string[]{"lan","醂爦嬾襕壈烂懒罱孄坔覧爤浨糷灠缆榄燗孏爁滥擥襽兰岚暕拦栏婪礷蓝繿钄阑躝欗澜褴漤斓籣囒灡谰览篮燣儖譋揽葻璼灆镧韊幱"},
            new string[]{"lang","羮塱硠烺螂誏郒琅郞朗艆勆阆蒗锒稂朖嫏廊蓈榔狼桹埌浪唥啷蓢樃欴朤郎躴"},
            new string[]{"lao","铹栳哰恅珯姥硓蛯佬鮱老憦軂涝烙耢酪嫪橑僗劳牢唠崂痨窂咾铑捞憥朥磱簩顟醪髝浶狫粩橯労"},
            new string[]{"le","忇勒泐叻乐仂阞氻鳓竻簕楽了砳玏韷"},
            new string[]{"lei","缧蘱鱩禷蕌颣頪纝塁嫘虆壨泪腂厽絫瘣罍礌鼺嘞擂酹镭类檑肋儡蕾磊傫垒诔耒雷羸轠累蘽癗藟櫐蘲鸓讄鑸蠝郲瓃畾矋銇頛蔂灅"},
            new string[]{"leng","塄棱楞堎愣睖踜薐冷碐"},
            new string[]{"li","栵搮砅茘赲娳蠡栛苙礼蒞珕厤暦瑮蜧悧峛囄蠫廲劙鑗李哩穲栃峲粴豊蟸欚朸岦俚瓑鲡蠇嫠瓅爏靋脷瓥犡轣讈囇麜婯鷅礰鱱塛盭檪濿醨屴鬴璃禲蛠蒚悷秝磿儮曞櫔鴗蜊疠猁栗刕郦轹理荔娌栎俪利隶砾枥戾例苈扐厯沥坜呖励俐傈蔾銐粚糎竰盠孷艃筣睝鳨菞砺蛎梸粝粒悡笠荲唳剓莅疬剺鲤澧黧詈跞溧鯏蓠蚸里凓醴鉝邌黎丽鵹唎篱篥藜罹鏫鯬锂狸吏立厉历褵力雳梨痢逦离鋫莉漓犁喱鹂錅蟍嚟缡鳢謧骊厘"},
            new string[]{"lian","湅媡萰浰堜瑓澰鰊纞磏羷錬翴濂嫾梿嗹匲覝劆匳噒聨聫縺鄻聮薕螊櫣蹥謰鬑鐮籨嬚濓鲢镰奁连帘怜涟莲联廉臁蠊敛琏练裢裣脸炼恋殓链楝潋蔹"},
            new string[]{"liang","蜽簗俍辌綡冫脼緉哴喨湸煷輌鍄啢粮凉两裲踉靓莨粱良魉椋涼梁俩亮墚谅辆晾量掚駺"},
            new string[]{"liao","嶚寮嘹嵺憀廖樛鹩敹暸寥蹽缭料嶛尥蓼钌潦蟧燎镣撂賿爒炓膋尞僚聊疗蹘鄝辽飉豂蟟簝廫竂撩膫髎窷璙獠"},
            new string[]{"lie","倈烮列鴷冽洌埒烈捩裂趔躐鬣爄綟咧猎犣埓劽聗姴挘毟睙巤颲儠鮤峢劣鱲猟浖蛚迾煭鬛茢挒"},
            new string[]{"lin","躏菻癝麟澟癛鳞拎僯鏻悋瞵霖辚遴嶙粼琳凜凛撛璘磷橉吝啉临林邻碄崊箖焛潾甐閵粦疄痳蹸躙晽驎蔺赁檩轥淋懔膦廪瀶壣翷繗暽斴"},
            new string[]{"ling","衑呤另令倰狑零刢坽夌龄姈詅彾聆昤朎皊砱秢竛婈岺泠铃凌瓴玲棂柃淩蛉苓菱岭囹灵伶羚翎琌绫麢鲮燯霝齢瀮孁蘦魿爧霗龗阾袊炩领笭崚醽裬鸰紷舲陵祾酃跉鹷軨掕閝蔆霊駖澪蕶錂"},
            new string[]{"liu","浏遛羀熘塯廇雡鬸溜刘流留琉飗旒橮馏骝榴瘤镏鎏柳绺锍六鹨硫疁镠沠畄旈裗媹嵧蒥蓅瑬磂駠嬼橊鐂熮珋桞驑鹠飅罶麍藰嚠駵鰡"},
            new string[]{"liwa","瓼"},
            new string[]{"lo","囖"},
            new string[]{"long","巄龓湰槞巃陇鏧霳爖礲襱垄窿蠬嶐豅躘鑨鸗壠竉哢梇贚拢蠪茏昽漋癃龙咙眬泷栊珑胧砻笼聋隆篭"},
            new string[]{"lou","耧屚甊喽蒌镂楼瘘漏陋篓搂嵝蝼塿遱髅娄熡鞻廔艛"},
            new string[]{"lu","簶觻騼鏴鵱鵦鯥鏕簵嗠簬蹗炉鴼螰錴蕗穋醁踛趢騄漉觮樚辂氇麓鹭簏璐潞胪戮虂露噜撸卢庐芦垆泸栌辘鈩箓膟路甪陆录枦玈鸬魲嚧轳瓐籚纑蠦赂逯镥橹鲁掳虏卤舮渌矑鹿琭禄颅鄜碌舻鲈睩峍獹勎娽淕硉菉椂硵盝彔稑塶廘摝粶蔍鑪熝勠澛髗黸挔捛塷樐磠瀂鐪圥侓鏀蓾坴"},
            new string[]{"luan","乱卵銮滦挛峦孪鸾栾脔羉癴圝奱曫虊圞癵釠娈灓"},
            new string[]{"lue","圙略锊掠鋢"},
            new string[]{"lun","伦仑抡纶囵轮沦论溣婨崙惀菕棆腀碖踚磮錀鯩稐崘埨"},
            new string[]{"luo","鸁锣椤逻萝荦癳镙脶詻饠剆臝攞曪囉洛鎯儸覼蓏攭攎箩罗骡泺蠃瘰頱倮螺络裸笿鮥纙峈猡珞骆摞落漯雒"},
            new string[]{"lv","梠絽郘焒鷜稆屢膐儢穞祣曥藘膢謱嵂慺葎氯膂屡侶榈滤捋馿侣寠绿率虑律履寽旅垏吕闾驴鑢箻勴繂缕爈偻櫖"},
            new string[]{"m","嘸"},
            new string[]{"ma","麻吗嘛蚂码蟆犸蔴妈马祃骂傌玛鬕溤犘杩遤鎷鷌鰢亇閁礣嫲嬤睰痲"},
            new string[]{"mai","麦迈脉卖唛売霢劢埋鷶买荬嘪霾霡佅"},
            new string[]{"man","鳗鞔瞒樠馒颟鬘熳满蛮姏蟃澷蔄獌僈矕鏋襔睌屘缦慲螨镘蘰蔓漫慢幔墁谩曼鬗"},
            new string[]{"mang","蘉痝杧杗邙壾蠎莾牤駹氓铓釯浝娏哤笀恾龒蟒忙芒盲茫硭莽漭牻"},
            new string[]{"mangmi","匁"},
            new string[]{"mao","蓩冃芼覒媢笷暓愗戼鶜軞枆鄮牦矛毛猫渵貌峁泖茆昴铆茂冒贸耄袤帽鄚楙瞀毷懋蟊髦锚酕旄冇茅蝐卯瑁"},
            new string[]{"mas","唜"},
            new string[]{"me","濹嚒嚰癦麽么庅"},
            new string[]{"mei","脄攗堳眛祙抺黣躾嬍媺嵄媄挴凂跊攟煝矀徾塺腜禖瑂葿睂湈珻栂苺燘毎霉枚玫眉莓梅媒嵋湄猸楣煤酶痗鹛没每美浼渼镁妹昧媚寐魅蝞篃睸镅坆眊脢郿袂"},
            new string[]{"men","暪虋璊菛閅椚満钔门玣扪闷焖懑们"},
            new string[]{"meng","甿蕄曚橗儚溕萠掹冡鄳擝矇懞獴鋂饛罞蜢虻萌盟甍瞢朦檬礞艨勐猛蒙鹲艋鄸懵蠓孟梦霥夣鼆鯭瓾顭靀鯍氋锰"},
            new string[]{"meo","踎"},
            new string[]{"mi","榓蔝銤孊灖峚宻幦塓渳漞熐蔤鼏冪醾淧麛尒尓穈冞蒾詸蝆麊葞爢醿鸍釄侎粎濗擟糜樒弭芈米蘼靡敉縻脒醚谜猕迷祢弥咪麋密藌櫁簚羃蜜嘧洣幂覛秘觅泌宓汨糸眯谧滵幎"},
            new string[]{"mian","矊腼麺麫糆葂愐喕偭湎矏缅矈櫋檰嬵蝒臱婂芇勔鮸汅眠渑面丏冕绵棉免沔勉眄娩"},
            new string[]{"miao","妙瞄鹋杪眇秒淼渺缈描邈竗庙篎喵苗媌嫹鱙藐庿"},
            new string[]{"mie","衊鱴鑖懱鴓薎搣孭吀幭蠛咩灭蔑篾"},
            new string[]{"miliklanm","瓱"},
            new string[]{"min","缗玟旼姄垊抿愍皿闽捪泯湣敏悯盿闵笢民岷旻苠珉黾蠠簢潣慜僶罠笽冧勄刡冺錉碈痻琝琘鍲崏鳘敯"},
            new string[]{"ming","佲榠茗鸣明名慏铭姳鄍覭洺眀朙眳嫇猽凕詺溟暝瞑螟酩命掵冥"},
            new string[]{"miu","谬缪"},
            new string[]{"mo","貉嚤尛魹橅嚩模妺礳懡抹麿魔摸谟嫫馍摹蘑磨末漠髍蟔爅纆耱貘默镆瘼墨黙蓦魩寞莫秣陌茉沫殁摩膜嬷貊絈劰圽怽帞昩枺皌眿砞莈藦粖擵袹蛨貃嗼塻銆嫼暯瞐瞙眽"},
            new string[]{"mol","乮"},
            new string[]{"mou","麰某劺恈洠鴾鍪牟蛑鉾哞谋侔眸蝥"},
            new string[]{"mu","拇暮穆坶霂鞪母毪亩睦姆幕木仫目沐牧苜钼募墓牡峔毣炑狇凩踇鉧畮砪畞慕牳莯氁畒蚞茻雮慔楘艒縸胟"},
            new string[]{"myeo","旀"},
            new string[]{"myeon","丆"},
            new string[]{"myeong","椧"},
            new string[]{"n","咹"},
            new string[]{"na","捺钠衲娜肭纳那哪镎呐魶蒳嗱乸雫袦笝豽軜貀拿靹"},
            new string[]{"nai","耏釢倷疓孻熋腉渿錼褦氖乃奶艿鼐萘耐柰奈"},
            new string[]{"nan","腩抩赧遖蝻妠萳囡男南难喃楠畘戁侽湳揇諵煵暔莮娚婻枬"},
            new string[]{"nang","灢齉攮曩馕囊囔嚢擃欜涳"},
            new string[]{"nao","脳恼垴蛲猱呶脑嫐挠匘悩夒檂嶩碯硇瑙孬峱巎淖婥闹巙"},
            new string[]{"ne","呢讷眲"},
            new string[]{"nei","氝内腇鮾鯘馁"},
            new string[]{"nem","焾"},
            new string[]{"nen","嫩"},
            new string[]{"neng","竜能螚"},
            new string[]{"neus","莻"},
            new string[]{"ngag","鈪"},
            new string[]{"ngai","銰"},
            new string[]{"ngam","啱"},
            new string[]{"ni","籾旎屔郳婗镾麑妮懝腻睨溺匿逆你拟誽伲鲵霓猊铌倪泥怩坭昵嫟淣尼嬺縌愵惄堄眤胒迡薿擬隬蚭觬蜺儞臡貎伱齯跜狔苨柅晲馜"},
            new string[]{"nian","埝拈跈年鲇卄捻哖辇撵碾廿黏秊鲶唸鵇蔫躎秥簐念"},
            new string[]{"niang","酿娘醸嬢"},
            new string[]{"niao","尿茑鸟脲尦嬲嬝袅褭嫋"},
            new string[]{"nie","嶭臲錜籋糱糵蠥踙敜闑孽嗫镊镍痆颞峊啮巕蹑嵲陧帇涅圼苶臬捏枿踂惗乜踗蘖囓聂"},
            new string[]{"nin","您囜脌恁拰"},
            new string[]{"ning","拧咛狞柠聍凝佞泞宁鬡儜寕寧寜薴甯鸋橣侫澝嬣"},
            new string[]{"niu","牜怓拗炄靵狃钮纽扭忸牛妞杻"},
            new string[]{"nong","农襛脓禯醲癑弄齈挵侬哝浓廾挊繷蕽欁憹秾莀"},
            new string[]{"nou","譨譳鐞羺啂耨"},
            new string[]{"nu","胬傉砮伮怒笯弩奴詉努驽孥"},
            new string[]{"nuan","餪奻煗暖"},
            new string[]{"nue","疟硸虐"},
            new string[]{"nun","黁"},
            new string[]{"nung","燶"},
            new string[]{"nuo","糯懦糥锘搦喏诺挪梛毭懧稬榒逽掿搻傩糑袲"},
            new string[]{"nv","籹衄恧钕女朒沑"},
            new string[]{"o","筽哦噢"},
            new string[]{"oes","夞"},
            new string[]{"ol","乯"},
            new string[]{"on","鞰"},
            new string[]{"ou","腢膒鴎櫙熰吘呕鏂塸蕅偶鸥瓯殴欧耦怄讴藕沤"},
            new string[]{"pa","爬耙琶杷袙葩趴筢帕怕妑舥帊苩潖啪"},
            new string[]{"pai","牌俳徘排箄派湃簰蒎拍輫哌鎃犤猅"},
            new string[]{"pak","磗"},
            new string[]{"pan","眫眅判鋬炍幋籓槃蒰膰畔盼碆柈鑻螌褩牉牓鵥叛爿泮攀盘磐蹒坢鎜襻詊潘頖沜縏瀊蟠冸溿鞶袢"},
            new string[]{"pang","沗乓旁厐逄彷胖耪螃舽庞覫霶肨鳑龎蠭雱膖胮尨炐夆滂趽"},
            new string[]{"pao","刨爮萢礟垉麭蚫褜奅咆麃泡嚗鞄軳跑脬抛炮匏铇藨袍穮疱狍庖"},
            new string[]{"pei","蓜浿斾笩醅陪馷怌衃胚攈伂俖棑毰姵陫霈呸柸培琣辔配珮旆帔佩沛裴锫赔阫"},
            new string[]{"pen","衯翸翉呠喷葐歕湓盆瓫"},
            new string[]{"peng","搒硼逬棚蓬鹏芃膨掽梈恲匉朋烹砰怦碰閛蟛漰蟚纄韼騯鬔鑝淎皏剻捧錋槰樥彭稝塳鬅髼摓堋軯輣抨韸塜椖弸莑竼挷磞駍憉嘭篷澎踫椪"},
            new string[]{"phas","巼"},
            new string[]{"phdeng","闏"},
            new string[]{"phoi","乶"},
            new string[]{"phos","喸"},
            new string[]{"pi","媲睥僻譬礔炋憵礕阰岯髬癖痞圮仳匹毞鼙貔蜱罴屁庀耚豾蚾鉟秛痺辟銔悂狉錍鮍脾箆苉擗磇嚭澼駓甓鸊郫錃噼蠯邳批伾丕隦壀篺螷簲啤鵧秠鷿疈脴睤嫓揊渒銢諀噽猈劈椑埤陴鴄蚍疲毗枇肶芘坯霹膍铍蚽砒豼焷伓腗披榌琵皮"},
            new string[]{"pian","鶣骗偏片魸徧翩骈篇媥腁楄楩貵谝囨賆蹁犏胼覑騈骿諚"},
            new string[]{"piao","漂嘌剽殍磦摽顠嫖票螵皫翲瞟醥彯篻骉瓢飘徱僄缥蔈旚魒"},
            new string[]{"pie","瞥暼鐅撆丿撇苤氕覕"},
            new string[]{"pin","馪汖朩穦矉拼贫嫔频礗姘驞颦薲品榀玭牝聘嚬拚"},
            new string[]{"ping","聠蓱甁娦玶郱岼呯竮艵砯涄幈冖甹帡屛慿萍瓶洴枰箳塀鉼焩倗硑帲泙鲆屏娉乒俜蛢平评凭坪苹胓蚲簈檘輧"},
            new string[]{"po","粕馞笸櫇钋嘙尀駊岶敀昢珀破哱魄湐淿溌岥尃巿洦皤謈髆鏺颇搫坡鄱泼蒪迫叵钷攴烞砶婆"},
            new string[]{"pou","掊裒犃剖郶颒哣咅捊抔吥"},
            new string[]{"ppun","哛兺"},
            new string[]{"pu","圤脯酺蜅抪駇莆匍噗铺曝箁菩炇巬仆巭痡葡扑浦蒲璞濮镤朴蒱埔擈普溥谱氆镨蹼圃舗襥剝檏鵏諩潽暜圑烳贌穙瞨獛菐墣"},
            new string[]{"qi","綦畦萁骐骑棋琦琪祺魕旗淇蜞蕲軝妻蛴槭麒憩器碛葺荠砌圻齐祁亓七漆乞嘁欺期萋戚桤栖凄柒鳍沏蹊绮邔麡玂契泣汽弃迄汔讫蟿綮唘起杞芑緀屺企舙稘迉倛气盵罊磜碶暣摖湇湆夡欫唭呇咠盀呮芞矵気闙簯諬婍啟啔捿栔竒缼碁釮慽猉渏掑帺蚚蚑鬿剘粸疧肵岓鶈魌霋諆磎慼愭崎斊櫀启桼軙朞騹艩纃籏鶀紪褀簱娸褄檱僛藄濝鲯螧踑禥蜝緕鵸棨岂奇歧攲郪旂埼祈玘炁噐岐耆其颀芪躩脐鬐"},
            new string[]{"qia","洽掐葜硈殎冾圶髂酠拤袷跒恰鞐"},
            new string[]{"qian","輤騝壍篟黚鬜鏲刋鑓蜸嵰鰬籖灊騚歬鐱鎆鬝仱岒拑乹欿偂軡媊榩攓谴钳乾掮箝潜黔浅肷仟遣钤缱欠芡茜倩堑嵌椠歉慊悭千悓阡棈扦芊迁佥岍钱牵虔铅谦愆签骞搴褰前歁钎奷杄谸蚈汘鹐鑯荕汧艌燫拪竏嬱茾濳粁顅傔墘皘蔳儙篏攑檶諐撁摼雃朁櫏攐"},
            new string[]{"qiang","嬙玱斨溬嶈牄羫漒猐墻琷獇羻唴摤墏蘠嗴锵鸧彊襁炝羟抢樯蔷嫱墙强跄镪戕呛羌枪戗謒腔蜣锖"},
            new string[]{"qianke","兛"},
            new string[]{"qianwa","瓩"},
            new string[]{"qiao","缲鞘窍撬翘桥悄硗跷鄡劁敲锹橇蹻乔荞峭谯憔鞒樵瞧巧愀俏诮侨癄譑鄥焳趬硚槗荍郻藮繑嘺摮陗犞躈僺髚嵪帩髜鐈趫幧墽頝墝踍峤"},
            new string[]{"qie","挈倢切茄且妾窃穕惬偼箧媫怯籡鐑藒踥緁蛪笡淁洯悏趄锲郄朅鯜癿倿匧聺苆厒"},
            new string[]{"qin","顉誛寑侵抋钦衾藽瀙菣赾笉嵚坅芹梫懃骎蠄瘽昑溱懄揿沁吣寝锓螓檎嶜噙濅嗪勤禽琴秦螼芩澿厪擒耹亲橬儭鮼寴庈唚珡菦捦鈙雂靲鳹埁"},
            new string[]{"qing","倾凊靑清媇暒棾硘甠夝鑋鲭卿情淸轻氢青樈濪儬碃掅圊箐蜻磬櫦庆謦勍请苘黥檠郬擎氰晴顷罄"},
            new string[]{"qiong","銎蛩琼筇茕穹惸瞏邛跫穷璚藭宆睘熍蛬竆藑橩憌焭焪儝赹笻桏卭舼"},
            new string[]{"qiu","酋遒鼽蚯蝤裘巯赇球逎楸俅泅虬求犰囚秋逑湭鵭鯄鮂銶觩蛷絿煪鰽皳邱鳅糗崷釻湫蠤搝丘盚梂璆訅唒浗紌肍萩朹坵鹙鞧鰌訄毬莍穐丠恘寈媝篍觓蝵殏蟗蘒龝叴扏玌緧釚"},
            new string[]{"qu","抾磲匚蕖渠璩麴蘧髷誳憈駆筁趣鰸衐觑阒去龋娶弆淭岴菃曲蠼衢癯氍取郥浀趍斪鼁麮岖耝诎呿瞿刞厺竬詓閴趋坥阹匤鸲朐区黢鱋蛐躯蛆祛屈驱劬璖佉匷蟝脥螶葋絇灈戵欋臞蠷翵鼩佢麹袪躣籧胠竘鸜鑺"},
            new string[]{"quan","硂埢姾鐉婘洤騡犈痊絟腃葲搼醛楾瑔觠汱湶韏矔勧琯圈全权诠筌荃蜷拳酄牶闎辁铨峑駩泉齤弮捲巻鳈虇牷悛佺烇券劝绻畎犬颧鬈椦蠸巏孉"},
            new string[]{"que","搉塙隺蚗傕墧崅敠蒛礭确炔缺瘸却皵雀榷燩阕阙鹊躤悫"},
            new string[]{"qun","逡宭裠麏夋峮囷裙群"},
            new string[]{"ra","亽罖"},
            new string[]{"ram","囕"},
            new string[]{"ran","袡苒卪冉燃髯肰蹨衻染蚦嘫柟繎姌珃媣蒅蚺呥袇然"},
            new string[]{"rang","鬤嚷獽壤攘让懹躟禳譲爙穰瓤壌瀼"},
            new string[]{"rao","遶襓犪饶荛桡扰隢娆绕"},
            new string[]{"re","惹蹃热"},
            new string[]{"ren","仞壬忎认刃腍朲荵秹綛躵仭屻韧肕讱衽稔饪轫妊纫任扨荏栠魜秂鈓牣鵀涊仁芢人忈栣忍纴"},
            new string[]{"reng","辸礽扔陾芿仍"},
            new string[]{"ri","日囸鈤驲"},
            new string[]{"rong","融容栄嵘髶冗榕熔蝾镕荣蓉肜駥褣傇穁瑢榵摉媶戎氄狨嫆嬫烿绒峵茸茙搈穃溶爃瀜坈曧宂"},
            new string[]{"rou","韖騥肉鞣蹂糅鰇揉瓇楺宍柔葇鍒禸粈鶔渘瑈腬"},
            new string[]{"ru","洳吺筎蒘蕠鴑嬬鴽曘褥鱬杁蓐鳰媷侞肗溽嗕缛扖込醹如蠕襦薷濡孺嚅儒袽茹桇颥入铷嶿汝鄏辱帤乳邚"},
            new string[]{"ruan","瑌壖堧耎偄渪愞嫰蝡瓀软朊緛礝阮腝碝"},
            new string[]{"rui","蚋蕊兊锐瑞蕤枘睿內橤芮甤壡緌蘃桵繠兌"},
            new string[]{"run","閠润橍闰"},
            new string[]{"ruo","鰯叒楉鰙婼鶸弱渃若鄀箬撋爇嵶偌"},
            new string[]{"sa","馺卅洒萨撒飒鎝靸攃蕯桬櫒仨脎隡"},
            new string[]{"saeng","栍"},
            new string[]{"sai","赛鳃毢噻嗮腮揌塞嘥簺"},
            new string[]{"sal","虄"},
            new string[]{"san","壭犙厁彡氵帴弎閐繖伞毶糂糁毵叁鏒散俕糤橵三馓"},
            new string[]{"sang","颡磉丧桑嗓桒槡褬鎟搡"},
            new string[]{"sao","瘙搔骚缫臊嫂髞掻溞鳋騒氉鄵矂懆扫"},
            new string[]{"se","色啬铯瑟穑廧涩瘷渋歮琗懎擌鏼濏璱繬穯轖譅飋濇"},
            new string[]{"sed","裇"},
            new string[]{"sen","森"},
            new string[]{"seng","僧鬙"},
            new string[]{"seo","閪"},
            new string[]{"seon","縇"},
            new string[]{"sha","痧裟硰鲨傻唼倽歃纱煞霎萐喢翜閯啥粆濈厦蔱鯋剎莎猀铩杀沙砂翣唦"},
            new string[]{"shai","酾繺簁筛晒"},
            new string[]{"shan","笘栅剼穇磰炶覢椫熌蔪羴檆曑敾睒晱樿摻軕笧脠狦杣圸邖苫僐山善珊埏钐衫櫼姗芟跚删煽謆凵譱単閄灗鐥墡赸杉骟蟮赡膳擅嬗舢鄯鳝閊扇疝汕讪陕闪膻潸缮"},
            new string[]{"shang","伤鬺螪扄謪上慯滳蔏殇觞墒熵裳垧商赏尚绱塲恦尙仩丄鑜晌"},
            new string[]{"shao","韒勺鞩鮹旓焼杓艄弰綤玿髾輎潲哨蛸稍袑绍烧梢筲芍捎韶少劭邵"},
            new string[]{"she","蔎弽渉蠂涻蛥檨賖韘赦舎设奢猞赊畲舌佘蛇摄厍騇社射涉麝慑輋滠欇舍"},
            new string[]{"shen","眘祳涁脤愼鋠瘮罙鯓柛兟榊薓甧罧訷鲹椮莘堔眒甡峷籸穼裑矤侺覾瞫曋頣駪宷昚弞邥鰰鉮籶鵢訠娠伔婶谂矧哂审沈神甚砷胂珅诜绅呻侁身伸申深瘆扟屾葚嘇鯅氠淰肾什妽谉燊蓡葠蜃椹慎渗綝"},
            new string[]{"sheng","偗鉎昇陞橳蕂榺墭貹剰晠渻鍟憴鼪阩焺湦陹曻竔珄殅泩枡斘呏譝甥剩盛圣鵿眚嵊绳晟笙胜牲声生升省"},
            new string[]{"shi","籂尸失师祏宲睗襫邿簭餝遾嬕舓诗鰘塒佦辻饣籭襹鶳遈鲺呞瑡蒒葹湤鸤煶鰤鮖礻鉂榁笶兘觢鼭呩篒鉽榯蒔溡嵵乨栻弒揓崼釈铈眡卋烒叓眂忯枾恀冟鍦眎鼫飠屍浉絁蓍鳾埘寔峕鲥炻豕恃拭贳舐弑嗜褷湜柿式事侍势视试饰室虱是実适轼逝谥释筮誓奭似蚀仕噬氏士驶始使矢施食竍拾实识时石十湿狮史示匙市世"},
            new string[]{"shike","兙"},
            new string[]{"shiwa","瓧"},
            new string[]{"shou","绶瘦痩膄夀獣授售兽狩受寿艏首守手收"},
            new string[]{"shu","綀鏣軗鱪鱰怷荗絉蒁襩錰糬鶐虪疋裋朮丨属濖鵨柕掓焂毺陎跾踈蠴瀭杸尗婌璹暏鼡潻癙鮛束塾熟暑黍署鼠蜀薯曙赎戍庶沭述树竖恕数腧墅漱澍术淑殳书倐孰纾叔枢姝倏抒梳菽鄃疏舒摅毹输蔬秫殊忄"},
            new string[]{"shua","刷耍唰誜"},
            new string[]{"shuai","衰蟀帅甩摔卛"},
            new string[]{"shuan","腨涮闩拴栓"},
            new string[]{"shuang","灀鏯縔霜樉慡孀爽騻双欆孇塽礵艭"},
            new string[]{"shui","睡帨脽涚閖税水谁裞涗氺"},
            new string[]{"shun","蕣橓鬊吮瞤輴瞚舜顺瞬"},
            new string[]{"shuo","鎙铄朔烁妁说矟硕搠蒴槊"},
            new string[]{"si","厮禩蕼儩澌鈻涘罳覗瀃貄凘釲丝肂洍梩牭柶撕娰孠価泤竢耜俟驷饲泗罒廝祀姒兕伺私寺飔嗣肆騃厶笥司愢四巳死汜斯糹泀俬恖咝媤灬亖思鸶缌偲蛳簛榹鐁騦蟴蟖蕬锶嘶楒銯鼶禠鉰禗虒"},
            new string[]{"so","螦"},
            new string[]{"sol","乺"},
            new string[]{"song","宋梥淞蘴倯蜙怂菘悚枀耸庺讼嵩诵送颂凇忪松娀竦硹憽檧駷濍餸傱嵷鎹柗愯"},
            new string[]{"sou","鄋锼飕薮搜廋馊擞瞍螋溲獀颾瘶櫢傁騪醙嗖蒐嗽廀捜嗾艘凁叟蓃"},
            new string[]{"su","觫圱璛摵簌蔌溯愫僳圲肃嗉谡粟速素涑塑粛遬趚榡遡溸嫊蹜傃樕殐珟櫯鯂稤苏塐膆夙玊俗稣酥窣憟鹔樎鱐藗縤餗鋉潥诉骕"},
            new string[]{"suan","狻算酸笇祘匴痠筭蒜"},
            new string[]{"sui","髓亗砕穟粋旞娞虽眭睢濉绥髄隧鐩譢鐆繸邃隋燧随碎遂谇祟岁繐穗毸挼歳煫荽缞瀡澻穂賥檖璲禭繀韢滖遀膸嬘襚瓍熣荾夊芕倠浽哸鞖"},
            new string[]{"sun","薞搎潠蕵鎨畃损箰巺槂榫笋飧荪狲孙摌喰隼"},
            new string[]{"suo","嵗嗦簔莏傞摍鮻羧睃梭桫挲娑唆趖髿暛鎈琑溑蓑鎻鏁逤蜶嗍缩所唢魦索琐锁"},
            new string[]{"ta","涾毾禢誻嚃錔崉濌蹹嚺狧鞜侤遝塌铊榙她沓祂咜鮙鳎他它蹋踏榻遢闼趿鞳獭撘逹塔溻澾阘褟躢挞"},
            new string[]{"tae","襨"},
            new string[]{"tai","跆呔台炱泰钛肽态囼太酞鲐汰苔邰胎骀漦軚咍抬箈孡坮薹炲嬯籉冭夳粏舦溙忲"},
            new string[]{"tan","顃菼榃憳嗿墰譠醰貚壜怹藫醈憛婒擹墵袒暺弾郯探醓叹毯钽坦忐檀潭谭锬碳覃炭谈昙坛瘫滩摊贪坍裧繵嘾痰埮憻舕僋湠傝璮"},
            new string[]{"tang","戃坣偒鼞鶶饄餹蝪薚糛曭煻矘摥篖糃磄膅鞺禟爣漟榶隚蓎嵣啺傏膛羰螗搪塘棠堂唐瑭镗樘铴汤漡簜蘯闣伖饧傥鄌赯镋趟烫溏耥嘡淌倘帑醣螳踼糖躺"},
            new string[]{"tao","涭醄轁鞱錭洮幍槄飸謟迯绹祹饕裪逃鋾搯駣騊韬焘滔慆掏绦涛弢蜪檮瑫夲仐讨桃嫍套萄詜淘梼啕陶鼗"},
            new string[]{"tap","畓"},
            new string[]{"te","蟘貣慝铽忑特"},
            new string[]{"teng","鼟縢籘漛藤滕邆駦儯誊疼霯驣腾幐虅"},
            new string[]{"teo","唞"},
            new string[]{"teul","朰"},
            new string[]{"ti","题鹈俶瑅缇提啼锑踢鍗迖体梯剔苐髢奃绨鶗剃倜悌涕逖惕替裼蹄鶙悐骵鷉鳀騠殢姼迏醍嚏厗蕛磃趧扌謕屉徲洟徥掦鷤漽鷈鬀遆罤嵜稊惿碮瓋偍崹戻褅惖珶籊"},
            new string[]{"tian","瞋畋觍黇睼湉鷏倎唺悿晪琠顚睓舚餂賟畑沺菾緂碵搷田天胋磌屇靝靔酟婖兲鷆淟添恬甜填阗忝殄腆舔掭璳鴫舑"},
            new string[]{"tiao","苕絩趒鎥樤鋚调萔鯈螩鞗窱聎芀脁岹祒蓚旫祧龆笤髫鲦窕眺粜晀跳蜩条迢挑佻嬥岧庣宨"},
            new string[]{"tie","贴萜铁帖餮蛈驖鐡鴩僣惵飻"},
            new string[]{"ting","涏颋侱侹渟鞓厅誔珽庁听烃廷亭庭脡艇聴梃挺霆蜓葶婷莛聤烶停娗圢鼮耓蝏厛榳楟筳嵉邒廰汀諪"},
            new string[]{"tol","乭"},
            new string[]{"tong","餇眮桐茼彤佟同仝通童曈朣勭鉖冂絧氃粡劏秱爞痛犝膧綂憅烔峂湩峝砼痌铜恸筒桶捅统瞳潼僮樋囲燑哃庝蓪嗵炵蚒酮晍浵"},
            new string[]{"tou","紏埱偸偷鍮敨黈头投骰透蘣钭妵"},
            new string[]{"tu","揬嶀鋵鵚鼵図峹庩堍梌堗稌塗嵞瘏筡蒤鈯圗捈酴图突秃凸荼途趃凃痜廜湥土菟吐钍兔唋涋捸徒屠瑹葖鵵圡汢腯鷵迌啚宊鷋鶟駼馟鵌涂鍎跿"},
            new string[]{"tuan","慱鷻褖圕湍鷒猯貒団褍团蓴抟疃彖鏄煓墥檲"},
            new string[]{"tui","弚駾蓷藬娧魋推颓腿退煺褪蜕隤蹪頺騩僓蘈忒蹆俀"},
            new string[]{"tun","囤豚饨臀暾軘吞畽朜黗豘屯膯噋霕蜳坉錪鲀氽汭"},
            new string[]{"tuo","跎酡鸵饦砣柁嘽拓托驼岮楕箨鮀唾橐柝託椭庹妥袉鼍駞槖碢莌咃騨脱沱侂牠鵎堶讬跅汑毤嫷袥乇紽坨鼧佗魠陀驮拖"},
            new string[]{"uu","曢聣躼袰毮朑癷辪鍂焑鎼艈歚屗徚稥虲毜蒊耂毝烪蝊鶑桛蓞洜燞焽鐢藔贘皼聁斏闧霻祍"},
            new string[]{"wa","韈蛙娲洼挖娃哇砙屲佤帓劸瓲咓邷攨鼃瓦姽溛袜腽窊畖漥"},
            new string[]{"wai","歪崴外咼竵喎"},
            new string[]{"wan","睕孯烷掔闗晼塆汍澫帵潫萖惋腕纨芄完玩顽宛豌婉蜿绾脘菀琬皖畹碗万晚琓鄤綄梚晩盌唍倇丸貦刓笂抏挽岏壪弯剜湾邜忨贎贃杤卐卍鍐鋔綩椀"},
            new string[]{"wang","汪魍尪忘妄旺望琞抂亡王网往枉罔惘辋徍蝄瀇誷莣盳尫蛧尩彺蚟忹徃菵棢焹仼"},
            new string[]{"wei","娓纬尾苇委炜伪洧唯玮伟潍嵬诿帷鲔涠桅闱违沩帏惟味鰖魏慰蜹蔚猬渭喂谓尉艉畏萎位未卫维韪围痿蒍猥隗胃逶趡韦为巍薇煨微頠隈偎威危湋葳韑軎煟墛熭媦菋苿斖颹壝儰蔿寪犚躛鍡轊浘讏蜼讆饖蘶鳚躗鏏緭藯褽螱鮇罻璏懀霨葨鰄癐僞嶶梶蝛縅詴囗椳鳂煀渨揻揋愄媙喴隇烓楲骩腲暐骪愇覣嵔洈崣捤鄬霺蓶犩覹癓鮠鍏芛徫"},
            new string[]{"wen","亠鴖璺温汶问稳紊瘟阌炆雯蚊呅闻纹刎文鳁吻抆闦呚呡桽脗穏妏顐閺鴍搵蚉鎾榲瑥塭饂殟彣豱砇珳鈫瘒馼魰鳼闅芠"},
            new string[]{"weng","翁齆蓊嗡鹟蕹瓮螉聬瞈暡嵡塕鎓奣"},
            new string[]{"wo","捼卧偓腛仴蒦雘瓁莴硪斡挝倭媉楃渥肟濣喔窝蜗我龌沃涡幄握瞃婑涹唩猧捰婐"},
            new string[]{"wu","鰞鴮螐弙洖箼洿乄鷡誈鯃剭禑莁珸瞴祦釫茣鹀呉毋窏璑芴譕武侮捂牾珷鹉舞兀勿务戊阢怃婺噁旿鹜吾雾扤痦杌焐晤悟误物忤骛无霿揾潕靰鋘幠乌圬污邬呜巫屋迕鼯庑妩坞伍仵诬五钨蜈浯梧芜吴鋈午屼鼿窹寤溩嵨奦逜粅悮悞矹忢齀岉霚伆躌甒儛錻瑦碔熓塢娬啎倵玝渞熃亾兦"},
            new string[]{"xi","犠貕豯豀礂餙钑廗凞繥鯑鵗糦觹緆嶲縘鏭隵熻莔鼰鼳酅觽蠵觿瞦焈莃氥悕屖怸徆俙肹卥覀桸渓瘜焁嬆琋赥釸惁焟睎翖鄎厀喜蒠榽唽橀矽欷惜悉牺浠晞息娭奚唏燨穸硒析昔希吸西汐兮夕潟綌郗锡螅羲熹歙樨雭嬉屣蜥熙淅僖烯皙溪舾翕粞稀犀晰傒菥屃熄躧盻椞塈釳绤欯恄怬忥呬赩匸隟矖蹝縰壐諰橲憙漇鈢葈釐卌蜤膝蟢枲嶍鸂爔巇窸噏豨趇饻係驨衋霼虩磶黖覤蕮澙犔熂徯騽卻禊霫椺蒵漝趘槢蝷宩舄鉩阋鎴繫碏扱舃嘻郋蟋鉨騱謵洗谿雟醯曦鼷习席袭觋媳隙檄郤鱚细饩系隰戏玺禧蓰葸飁徙"},
            new string[]{"xia","谺颬縖傄筪廈梺疜圷丅鶷騢鍜赮縀碬鰕溊陿舺翈祫珨烚陜炠蕸峡辖瑕暇遐硖欱柙下狎侠匣瞎虾郃狭敮懗夓鏬螛諕昰霞閕黠徦叚罅夏吓捾舝"},
            new string[]{"xian","羬伣啣狝幰櫶伭忺铣弦繊憸韯嘕僲珗苮杴韱奾馦仙先纤氙祆籼莶掀跹佡娹饀癎輱諴誸澖嫺甉蛝褼婱鲜胘咞鱻鶱纎攕廯孅蹮蚿霰苋现线限宪陷馅缐羡酰腺燹仚盷屳譀壏鰔礆顈堿献鹇蘝暹闲贤咸涎娴舷岘痫县嫌冼显险猃蚬筅跣藓锨衔鼸晛涀睍絤粯僩鋧誢瀗糮臔娨錎豏撊僴赻橌毨鑦贒娊険尟尠搟禒鍌臽烍垷顕哯姭灦韅玁攇藖陥"},
            new string[]{"xiang","厢巷庠翔享响饷飨想镶向详项象像橡蟓瓖缃鲞蘘曏乡芗相祥湘閧勷傢迒箱襄骧闂葙闀香鱌嚮珦鱶姠缿萫勨嶑鐌銄襐楿蠁晑郷廂膷忀麘亯鱜佭栙絴跭欀鄊蚃"},
            new string[]{"xiao","销骁枵笹哓叜宵枭傚笑萧歒燺謼轇消薂校効洨蟏猇鸮虓譊崤绡逍硝潇箫霄啸嚣效淆小晓筱孝肖哮魈皢謏筿誵訤虈囂咲斆鷍恔涍滧詨嘋誟嘯斅髇髐熽毊宯庨灲恷婋焇痚痟硣窙歊踃蟂侾憢灱鴵蟰藃簘穘膮彇獢撨"},
            new string[]{"xie","邂榝慀亵勰譮鬹缬齛鞋写泄泻绁卸屑繲械孈渫谢榍榭廨懈獬薤燮蟹躞瀣偰些灺卨屟屧澥屭撷携寫觟楔搚谐蝎协邪胁歇斜偕炨夑絬屓徢焎禼媟娎塮褉噧韰薢齘齥齂躠炧暬糏旪缷嶰垥恊拹脋衺翓愶瑎劦讗祄伳嗋龤綊襭燲蝢緳膎熁藛"},
            new string[]{"xin","昕新芯心忻信歆薪馨囟衅锌欣鑫焮辛歀鬵伩顖舋孞阠潃伈邤馫嬜噺惞盺杺妡訫枔"},
            new string[]{"xing","陉悻婞荇性幸姓杏擤醒巠型坙形邢刑腥猩惺星兴倖铏骍硎坓哘娙裄銒鋞涬臖郉緈皨觲曐謃篂觪蛵瑆煋垶侀洐"},
            new string[]{"xiong","熊凶兄匈芎讻汹雄賯诇兇赨胸忷哅恟"},
            new string[]{"xiu","貅庥修宿滫鸺咻馐髹朽秀岫绣袖琇锈溴蓨羞茠嚊嗅臰休鏥飍繍珛璓齅糔綇苬螑鮴俢烋烌脙樇銝髤鎀臹"},
            new string[]{"xu","婿酗续绪勖洫吁恤叙旭滀戌糈栩盱胥须胊诩序掝歔谞諿恓繻许需墟溆訏絮蓿蓄煦嘘欻虚圩顼醑徐汿烼珬烅殈欰盨沀喣瞁垿朂訹続槒鄦盢伵聟稸緖藚潊幁旴疞欨侐媭暊揟楈窢稰魆噓冔縃蕦偦歘譃魖驉虗珝嬃"},
            new string[]{"xuan","萱选狟懁喧揎暄儇玄痃悬轋漩璇癣泫炫绚眩铉渲楦碹煊旋烜宣轩鋗瑄禤翾衒镟澴谖镮怰暅檈贙嫙颴蜁玹暶昡袨琄鞙縼愋鰚繏蔙愃譞睻昍媗吅蓒箮翧蝖諠鍹駽蠉塇"},
            new string[]{"xue","薛泶靴踅雪鳕血谑学削穴峃茓鸴噱勪滈艝觷鷽樰膤轌怴狘桖烕瀥燢趐疶學袕岤坹乴斈蒆雤瞲嶨"},
            new string[]{"xun","醺寻巡曛旬埙驯询峋恂鶽薰獯熏勋噀郇荨讯咰洵窨燻鲟浔彐噚蕈巽殉逊徇汛紃训鄩循荀珣迅侚伨卂鱏狥襑顨璕蟳迿訙奞殾賐攳樳鑂愻駨坃蔒薫嚑壎臐矄蘍揗爋纁杊桪燅偱壦勲"},
            new string[]{"ya","姶雅亚讶迓垭娅砑鶕揠堊哑崖磍氩鸭呀压丫押睚衙疨鸦牙伢岈芽厓琊蚜涯桠齾劜穵襾犽俹掗聐稏庌蕥圔庘齖漄瑘堐笌鐚铔孲圠厊"},
            new string[]{"yan","喦挻顑炏豣豜恹屵鳱偃筵鼹魇演罨琰彦掩砚匽俨奄兖檐颜蜒眼焰胭赝燕谳酽滟厌焱衍堰谚验艳晏宴唁雁讠腌湮阉菸焉淹鄢厣崦剡葊湺闫郾蝘黡烟觃咽嫣研炎沿盐餍妍延龑严阎言岩醼嚥鶠黤齞黬黭巘鰋齴隒妟曮甗檿躽噞椼渷棪揜扊愝牪褗酀驠灎墕觾嵃鷃騴軅艶嶮験姲曕鬳嬊暥鳫椻葕喭焔偐嬿黫嵓嵒訮琂娮莚崄娫郔綖円狿篶樮嶖漹偣珚剦熖兗昖麣乵麲夵塩礹欕巚巗壧麙櫩詽壛揅厳厃虤蔅碞抁楌巌"},
            new string[]{"yang","阳洋烊疡佯炀杨旸徉羊扬鞅鸯秧殃央仰卬潒泱钖歍蛘垟漾样恙怏痒氧养颺傟炴岟奍佒懩鸉霷坱軮慃氱駚攁礢羕詇紻鰑羪胦楧鐊咉抰鉠雵阦劷氜輰昜羏珜眻崸諹飏姎鴹"},
            new string[]{"yao","咬峣耀窈舀药要鹞曜侥靿鳐飖繇垚嚙撽崾轺邀爻尧肴钥杳妖吆夭腰幺姚珧窑傜谣徭摇遥瑶闄鴢榚溔婹偠柼狕殀騕矅苭穾袎窔筄詏熎覞獟鼼蘨曣讑薬訞仸祅岆喓葽楆尭倄烑揺磘枖嗂顤嶤嶢餆榣暚摿猺媱"},
            new string[]{"ye","腋靥倻铘暍殕枒谒痷熀噎冶液耶椰也爷野烨叶曳页邺夜晔业揶掖擪澲歋擛皣瞱嶪嶫曅餣擫曗瞸爗鸈驜僷鵺擨馌窫鐷潱礏鎁吔亪埜鄓漜壄抴枼洂枽嘢蠮墷"},
            new string[]{"yen","岃"},
            new string[]{"yi","扡泆勚廙彵蓺扆苢簃夷椸匜詑宧怿呓役抑译邑疫峄亦易绎诣驿奕佁佾亿矣苡舣蚁倚椅佚义异弋刈忆艺仡羿旖翼熠镒劓殪燚弈翳瘗臆癔镱懿賹诒薏逸轶悒挹益谊埸毅翌蜴意溢缢肄裔议翊咿怡宜沂圯仪黟噫漪揖壹铱猗迤焬箷戺钇鉇屹依医衣伊一洩欥拸錡歖痍祎饴彝嶷疑颐遗移乙酏胰已以贻咦姨荑眙亄鈠跇詍焲湙棭殔獈痬竩裛勩嫕榏晹膉埶潩殹帠浂玴衵唈垼浳袣羛墿敡靾萟訲訳豙豛釴隿陭鶃燡燱貖藙贀繶豷霬黓鶂曎鶍瀷蘙襼鷧虉鷾齸鯣嬑撎槸熤熼瘞鹝鹢帟歝骮檍曀澺瘱瞖穓艗螠寱駅圛媐衪峓弬恞瓵迻扅栘彛萓冝椬羠蛦暆跠頉熪嶬袘嫛兿弌俋衤壱洢畩郼侇蛜宐稦嬄夁瑿檹譩黳乁辷欹杙齮匇肊阣伇芅伿劮轙曵苅耴呭呹怈枍炈頥辥坄鸃顊寲顗鏔籎觺攺庡笖逘礒敼檥偯鳦鉯鈘旑崺輢"},
            new string[]{"yin","龈铟喑堙吟垠引狺霪寅鄞银蚓吲尹饮印茚夤氤胤隐淫堷鮣廕嚚崟瘖溵愔瘾裀骃因輑殷訢玪梀縯阴姻洇茵荫音囙鈏誾檭淾靷鷣璌蔩噖訚朄湚碒殥讔闉鈝憗癊酳猌洕懚趛櫽蘟螾濥嶾檃垽訡緸蒑禋歅絪鞇秵凐栶韾侌阥乚欭筃荶垔諲崯訔粌烎峾噾泿犾霠濦圁婬霒"},
            new string[]{"ying","罂俓硬蓥潆蝇嬴瀛颍颖影滢映赢媵溁籝撄应嵤韹夃瘿茔瑛缨郢婴楹璎鹦膺嘤迎樱盈莺荥英荧莹萤营萦鹰褮锳甇蝧賏罃嫈偀噟煐朠绬渶愥媖桜応珱颕濴藀攍攚瀯灐籯濚梬櫿摬巊廮鐛膡譍霙碤矨瀴韺鶧灜孾濙蘡礯譻鑍蠳溋僌孆鷪萾営蛍盁軈蝿"},
            new string[]{"yo","哟唷"},
            new string[]{"yong","镛泳揘雍用踊蛹恿涌勇俑鄘佣拥痈慵咏邕庸墉臃甬永喁饔鳙壅苚柡彮愑硧悀塎愹慂鲬砽醟噰埇禜槦郺栐嗈滽牅澭鰫雝廱灉鷛癕嫞勈"},
            new string[]{"you","佑黝祐幼囿狖右又宥侑诱蚴釉麀遊鼬獶脩尢牖耰油悠幽呦攸由忧铕优尤邮柚疣莜友莠羑酉犹有莸蝣猷鱿游蚰铀卣蜏禉蒏湵脜聈羐槱貁庮苃梄孧糿哊姷峟迶亴酭戭楢丣唀嚘邎褏駀尣怮泑鄾懮瀀櫌纋沋逰褎滺怣輶輏櫾訧偤逌秞峳"},
            new string[]{"yu","頨齵衘貗丂御桙肀翑汚鬱禦燏遹滪矞棫敔髃嵎貐欲饫育郁彧昱狱峪浴钰腴域虞淯谕阈喻寓裕鹆榆愚聿预雨驭玉龉窳瘐鄅庾圉圄语妪俣芋羽屿宇伛与蝓舆窬觎揄禹悇瘀渝淤迂纡愈煜蓣郚逾毓邘澚蜮豫鐍燠媮鹬鬻痏遇誉禺愉嵛雩隅萸渔馀谀狳娱于竽予俞鱼臾盂於玙欤妤余瑜舁緎焴稢罭琙蒮嶎噊蜟輍棜俼戫堉芌忬秗茟繘喐稶棛惐袬逳喅喩庽砡軉礜欎驈籞鱊鐭欝轝灪爩鱮獝捓酑鸒儥薁鋊鳿錥鴥霱鴪蓹礇篽醧櫲饇蘛鴧娛褕睮艅牏歈楡骬硢蘌湡堣崳堬歶扵燰迃陓虶唹釪亐娯玗衧兪茰荢畭箊羭鸆俁挧祤偊鷠萭寙斞楰瑀噳斔麌匬澞楀雓蕍懙螸鍝礖旟籅騟鯲鮽嬩鰅"},
            new string[]{"yuan","院垣辕橼螈远源怨鼋媛瑗愿裷輐喛苑爰渊箢元员园猿眢鸳塬原圆袁援缘沅鸢鹓湲囦嫄芫羱冤涴蜎薗厵鶰鶢騵邍謜蝯裫鎱盶逺夗肙妴禐蒝褤噮蝝衏灁渕渆渁葾棩蒬縁嬽鼘鼝榬溒薳駌剈邧媴鈨圎酛厡笎榞"},
            new string[]{"yue","趯曰约刖岳悦阅跃粤越樾龠瀹黦扚月蛻籰鸑玥矱焆钺嬳恱鸙龥禴爚蘥籆篗戉軏曱跀箹岄抈礿蚎捳"},
            new string[]{"yun","榅恽氲殒耘郧狁陨允孕昀郓匀晕酝愠缊韫韵熨蕴运畇媼緷沄赟芸妘纭云薀夽磒玧喗阭褞抎霣齫齳傊賱餫韗鈗繧枟抣蒀縜蜵馻蒕奫馧勻伝囩眃秐贠耺愪筼蒷熉橒篔呍"},
            new string[]{"za","咋噈咂杂砸匝拶沯囐雥雑沞鉔韴磼"},
            new string[]{"zai","崽载再甾在栽哉灾仔宰傤儎洅扗賳睵溨渽"},
            new string[]{"zan","昝瓒錾赞暂趱攒咱簪糌鐟儹篸兂饡禶讃灒酂鄼儧偺鐕瓉"},
            new string[]{"zang","臧葬脏匨蔵奘驵牂赃弉臓塟賘羘銺"},
            new string[]{"zao","枣蚤澡藻唣皂躁早噪燥造灶璪謲慥簉譟遭糟凿薻趮竃艁蹧璅栆醩梍煰"},
            new string[]{"ze","则责帻昃仄赜箦舴笮庂迮泽择厠萚啧襗謮蠌瞔鸅汄昗捑崱夨啫皟溭伬択沢泎蔶樍歵"},
            new string[]{"zei","鲗贼鱡蠈"},
            new string[]{"zen","怎谮譛囎"},
            new string[]{"zeng","增赠矰甑罾憎锃缯増鱛譄磳璔熷鄫曽縡橧"},
            new string[]{"zha","扎蚱痄咤潳乍砟眨铡闸轧蹅齇楂渣揸喳哳炸吒榨札鲝剳箑柞冊揷挓诈牐紮搾喥喋甴箚醡宱灹踷譗蚻迊觰譇鞢皶偧抯齄"},
            new string[]{"zhai","摘斋牴啇宅翟瘵窄骴债砦寨亝夈榸粂鉙厏捚"},
            new string[]{"zhan","欃湛蘸亶邅绽鳽占椾餰鳣霑沾毡旃粘譧鹯詹谵战飐站斩展盏崭搌辗佔栈瞻菚醆偡嫸戦嶘輚虥轏桟嶃虦惉琖蛅趈閚噡讝旜饘驙魙薝栴"},
            new string[]{"zhang","镸嶂仧账幛障胀掌彰漳獐樟鄣长章蟑瘴仉涨仗丈璋杖帐嫜礃张幥鞝鐣扙粀痮瞕涱瘬墇弡騿鏱餦暲遧蔁粻慞傽鱆"},
            new string[]{"zhao","兆钊招昭爪照召找诏赵笊棹曌肇罩鼌沼狣肁旐罀箌燳鮡佋羄瑵瞾皽爫鍣窼駋鉊盄巶妱枛垗"},
            new string[]{"zhe","詟驝聑摂柘着磔蜇遮折哲辄蛰谪辙者浙鹧蔗锗这褶赭晣嚞謺鮿襵淛樜悊嫬蟅厇啠矺砓籷埑粍歽袩虴"},
            new string[]{"zhen","帧獉纼枮鬒鈂真謓迧塡珍箴榛震蓁鼑甄斟祯阵臻桢振浈侦圳针贞稹缜疹沴砧镇胗轸畛朕赈枕诊鍼鸩眕縥駗覙裖萙昣袗辴絼聄黰挋栚眹姫揕抮蜄誫鋴鎭塦葴帪栕眞敒桭弫寊屒搸蒖鉁靕轃酙殝籈鱵錱薽澵樼潧禛瑧"},
            new string[]{"zheng","睁烝铮筝脀拯幀钲症正整证诤郑蒸政狰峥挣争怔嶒征塣鬇氶抍糽篜崝晸愸鴊鯹踭鮏炡凧佂姃眐崢聇媜揁徰"},
            new string[]{"zhi","窒嚔媞秩致贽轾掷痔膣踬之支卮汁芝吱枝知织置薙鸷职觯雉稚骘蛭痣滞智彘肢沚植絷絺跖摭踯止只椥址值纸芷祉咫指枳轵殖黹旨胝茝蚔軽亊樲夛眰摕拞埴祗胵鶨脂蜘执侄直遟歭酯栀誌至杫趾质郅峙栉陟帜桎治锧稙滍铚畤梽厔恉扺挚志忮豸制炙帙貭狾秲觗崻袠翐紩秷猘徝廌乿熫晊徏娡祑洷挃庢垁偫懥妷扻俧豒驇騺豑礩鯯瓆覟懫跱劕儨鴙駤隲瀄旘鋕憄潌槜櫍搘釞聀値秇鼅鴲謢鳷馶榰禃禔禵秪祬疷倁衼胑秖秓徔庤芖綕洔襧藢樀訨疻蘵砋瓡茋汦帋坁膱淽漐馽嬂蟙軄凪劧樴"},
            new string[]{"zhong","肿重众仲踵冢伀种螽衷蝩緟褈舯钟盅终忠中鍾喠狆歱煄瘇媑妕茽衶蚛堹筗銿籦鼨諥偅彸尰鴤刣妐汷迚泈炂柊衳幒蔠锺螤乑"},
            new string[]{"zhou","宙荮州诌纣洲粥妯轴碡肘帚咒绉啁胄舟皱酎骤籀侜辀週赒盩甃椆薵昼周紬伷嚋騆疛晭鯞冑粙詋僽駎駲籒菷籕郮睭炿霌珘洀婤徟淍矪喌銂輖烐"},
            new string[]{"zhu","烛猪麈蛛槠潴橥渚竺茱逐舳瘃躅主拄竹宔尌逫篴蕏朱註珠砫株蠋硃侏诛邾洙铢纻炷苎助祝翥注贮杼柱住诸箸铸筑蛀著疰伫瞩嘱煮驻秼莇竚祩眝殶柷壴迬紸濐馵坾羜鯺嵀詝跓軴飳墸樦鋳麆霔篫鉒鮢炢罜劯袾絑駯鴸鼄蠩笁茿蠾蝫钃笜爥欘曯斸灟劚鱁蓫"},
            new string[]{"zhua","抓髽膼簻檛"},
            new string[]{"zhuai","跩尵顡拽睉捙"},
            new string[]{"zhuan","僝塼漙馔篆撰篿转颛砖专腞啭竱灷堟蒃瑑襈籑転譔蟤諯瑼嫥鄟叀孨専"},
            new string[]{"zhuang","贑壮装桩庄妆状幢撞戅獞庒焋梉糚湷荘娤"},
            new string[]{"zhui","隹腏鎚倕追骓椎醊膇锥坠缀惴缒赘埀硾諈錗鑆轛礈鵻餟畷甀"},
            new string[]{"zhun","窀谆准迍衠甽肫綧凖宒圫稕"},
            new string[]{"zhuo","酌準藋镯濯擢琢诼浞浊斫茁灼涿啄桌捉倬拙卓剢鉵禚椓鷟棁籱硺諁窡撯斲劅櫡謶鵫斱籗罬坧墌蠗琸矠擆棳晫窧穛穱蠿圴叕妰丵烵娺梲斮犳"},
            new string[]{"zi","紫梓笫耔滓姊胾子秭訾字自恣眦牸嗞镃籽趑鼒渍赀兹孜菑龇穧鲻跐泚薋姿资淄缁谘孳嵫滋辎蓻锱髭咨橴頿齍吇杍矷胏虸釨剚榟茡倳鶅訿鰦茊鍿姕沝栥紎崰秶赼鈭湽葘鄑孶禌澬趦輺椔"},
            new string[]{"zo","唨"},
            new string[]{"zong","腙熜疭緫粽纵偬总踪棕综宗鬃搃揔惣蓗鑁捴摠燪鍯錝倊猔瘲鯼熧昮緃倧骔堫嵏嵕惾猣翪嵸鯮緵艐蝬踨磫豵鬷葼"},
            new string[]{"zou","辶緅鲰陬奏偢揍走鄹郰诹驺搊邹赱鯐黀箃棸齺媰掫"},
            new string[]{"zu","阻伹族鉃镞诅卒足租组綷俎怚祖顇紣伜菹靻鎺珇爼箤傶卆崪哫踿"},
            new string[]{"zuan","欑躜缵纂钻攥赚纉劗籫繤"},
            new string[]{"zui","醉嶉嘴檇晬最罪蕞睟觜脧嫢欈檌嶵栬祽酔絊穝璻噿嶊纗蟕樶厜酨稡酻枠"},
            new string[]{"zun","遵栫樽鳟僔撙跧尊捘銌譐嶟鷷鐏鶎繜噂"},
            new string[]{"zhui","隹腏鎚倕追骓椎醊膇锥坠缀惴缒赘埀硾諈錗鑆轛礈鵻餟畷甀"},
            new string[]{"zhun","窀谆准迍衠甽肫綧凖宒圫稕"},
            new string[]{"zhuo","酌準藋镯濯擢琢诼浞浊斫茁灼涿啄桌捉倬拙卓剢鉵禚椓鷟棁籱硺諁窡撯斲劅櫡謶鵫斱籗罬坧墌蠗琸矠擆棳晫窧穛穱蠿圴叕妰丵烵娺梲斮犳"},
            new string[]{"zi","紫梓笫耔滓姊胾子秭訾字自恣眦牸嗞镃籽趑鼒渍赀兹孜菑龇穧鲻跐泚薋姿资淄缁谘孳嵫滋辎蓻锱髭咨橴頿齍吇杍矷胏虸釨剚榟茡倳鶅訿鰦茊鍿姕沝栥紎崰秶赼鈭湽葘鄑孶禌澬趦輺椔"},
            new string[]{"zo","唨"},
            new string[]{"zong","腙熜疭緫粽纵偬总踪棕综宗鬃搃揔惣蓗鑁捴摠燪鍯錝倊猔瘲鯼熧昮緃倧骔堫嵏嵕惾猣翪嵸鯮緵艐蝬踨磫豵鬷葼"},
            new string[]{"zou","辶緅鲰陬奏偢揍走鄹郰诹驺搊邹赱鯐黀箃棸齺媰掫"},
            new string[]{"zu","阻伹族鉃镞诅卒足租组綷俎怚祖顇紣伜菹靻鎺珇爼箤傶卆崪哫踿"},
            new string[]{"zuan","欑躜缵纂钻攥赚纉劗籫繤"},
            new string[]{"zui","醉嶉嘴檇晬最罪蕞睟觜脧嫢欈檌嶵栬祽酔絊穝璻噿嶊纗蟕樶厜酨稡酻枠"},
            new string[]{"zun","遵栫樽鳟僔撙跧尊捘銌譐嶟鷷鐏鶎繜噂"},
            new string[]{"zuo","做作坐左座昨凿琢撮佐笮酢唑祚胙怍阼柞乍侳咗岝岞挫捽柮椊砟秨稓筰糳繓苲莋葃葄蓙袏諎醋鈼鑿飵嘬阝"}
        };
        #endregion

        /// <summary>
        /// 将汉字转成全拼
        /// </summary>
        /// <param name="str">汉字字串</param>
        /// <returns>全拼</returns>
        public static string Convert(string str)
        {
            if (str == null)
                return null;

            Encoding ed = Encoding.GetEncoding("GB2312");
            if (ed == null)
                throw new ArgumentException("没有找到编码集GB2312");

            int bh = 0;
            char[] charary = str.ToCharArray();
            byte[] bAry = new byte[2];
            StringBuilder rtnSb = new StringBuilder();
            for (int i = 0; i < charary.Length; i++)
            {

                bAry = ed.GetBytes(charary[i].ToString());
                if (bAry.Length == 1)
                {
                    rtnSb.Append(charary[i]);
                    continue;
                }
                bh = bAry[0] - 0xA0;
                if (0x10 <= bh && bh <= 0x57)//是gb2312汉字
                {
                    bool isFind = false;
                    for (int j = 0; j < _Allhz.Length; j++)
                    {
                        if (_Allhz[j][1].IndexOf(charary[i]) != -1)
                        {
                            rtnSb.Append(_Allhz[j][0]);
                            isFind = true;
                            break;
                        }
                    }
                    if (!isFind)
                        rtnSb.Append(charary[i]);

                }
                else
                    rtnSb.Append(charary[i]);
            }

            return rtnSb.ToString();
        }
        /// <summary>
        /// 汉字转简拼
        /// </summary>
        /// <param name="str">汉字字串</param>
        /// <returns>简拼</returns>
        public static string ConvertToSimple(string str)
        {
            if (str == null) return null;
            Encoding ed = Encoding.GetEncoding("GB2312");
            if (ed == null)
                throw new ArgumentException("没有找到编码集GB2312");

            int bh = 0;
            char[] charary = str.ToCharArray();
            byte[] bAry = new byte[2];
            StringBuilder rtnSb = new StringBuilder();
            for (int i = 0; i < charary.Length; i++)
            {

                bAry = ed.GetBytes(charary[i].ToString());
                if (bAry.Length == 1)
                {
                    rtnSb.Append(charary[i]);
                    continue;
                }
                bh = bAry[0] - 0xA0;
                if (0x10 <= bh && bh <= 0x57)//是gb2312汉字
                {
                    bool isFind = false;
                    for (int j = 0; j < _Allhz.Length; j++)
                    {
                        if (_Allhz[j][1].IndexOf(charary[i]) != -1)
                        {
                            rtnSb.Append(_Allhz[j][0].Substring(0, 1));
                            isFind = true;
                            break;
                        }
                    }
                    if (!isFind)
                        rtnSb.Append(charary[i].ToString().Substring(0, 1));

                }
                else
                    rtnSb.Append(charary[i].ToString().Substring(0, 1));

            }
            return rtnSb.ToString();
        }
    }
}
