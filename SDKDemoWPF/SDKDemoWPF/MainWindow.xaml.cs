using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media.Animation;

// MainWindow.xaml 뷰에 대한 조작 코드
// 각 엘리먼트의 xName 속성을 변수명으로 해서 접근 가능.

namespace SDKDemoWPF
{
    public partial class MainWindow : Window // WPF 프로그램 창 객체
    {
        //프로젝트 경로
        static String projectpath = System.IO.Directory.GetParent(System.Environment.CurrentDirectory).Parent.FullName;

        //dll 경로
        static String dllPath = projectpath + "\\ZDLL\\x64";

        //사진 비교 이미지 초기 경로 설정
        String imgDir = projectpath + "\\image";
        String img1Path = projectpath + "\\image\\i000qa-fn.jpg";
        String img2Path = projectpath + "\\image\\i000ra-fn.jpg";

        //사진 모드 Init 여부
        String isinit = "OFF";

        //선택된 사진 경로
        String img1PathC = projectpath + "\\image\\i000qa-fn.jpg";
        String img2PathC = projectpath + "\\image\\i000ra-fn.jpg";

        //동작 모드 
        // 0: CPU Mode, 1: CUDA GPU Mode, 2: MyRIAD VPU Mode
        int mode = 0;


        public MainWindow() //프로그램 시작시 가장 먼저 실행될 생성자.
        {
            InitializeComponent();

            //프로그램 초기화 로직
            //사진 비교 모드의 초기 사진 화면 지정
            imgPic1.Source = new BitmapImage(new Uri(img1Path));
            imgPic2.Source = new BitmapImage(new Uri(img2Path));
            img1name.Text = img1Path;
            img2name.Text = img2Path;

        }

        private void Picturebutton(object sender, RoutedEventArgs e)
        //picture 모드 버튼 클릭 이벤트
        {
            maintitle.Text = "Compare Pictures"; //화면 제목 변경
            
            //모드 전환 버튼 활성화
            picturebtn.IsEnabled = false;
            cambtn.IsEnabled = true;

            //보여줄 페이지 설정
            picturepage.Visibility = Visibility.Visible;
            campage.Visibility = Visibility.Collapsed;

        }
        private void Cambutton(object sender, RoutedEventArgs e)
        //CAM 모드 버튼 클릭 이벤트
        {
            maintitle.Text = "Compare CAM";//화면 제목 변경

            //모드 전환 버튼 활성화
            picturebtn.IsEnabled = true;
            cambtn.IsEnabled = false;

            //보여줄 페이지 설정
            picturepage.Visibility = Visibility.Collapsed;
            campage.Visibility = Visibility.Visible;
        }

        private void InitButton(object sender, RoutedEventArgs e)
            //init 버튼 클릭 이벤트
        {
            ChangeInitStatus();
        }

        private void ChangeInitStatus()
            //init 여부에 따른 화면 변화 로직 분리 메소드
        {
            if (isinit.Equals("OFF"))
                //init이 안된 상태면
            {
                expansiontestbtn.IsEnabled = true;
                basictestbtn.IsEnabled = true;
                performtestbtn.IsEnabled = true;
                initbutton.Background = new SolidColorBrush(Colors.LimeGreen);
                isinit = "ON";
                initbutton.Content = isinit;
            }
            else //init이 되어있는 상태면
            {
                expansiontestbtn.IsEnabled = false;
                basictestbtn.IsEnabled = false;
                performtestbtn.IsEnabled = false;
                initbutton.Background = new SolidColorBrush(Colors.Red);
                isinit = "OFF";
                initbutton.Content = isinit;
            }
        }

        private void Btsbuttonclk(object sender, RoutedEventArgs e)
        //기본 테스트 버튼 클릭 이벤트
        {
            
        }
        private void PtsButtonclk(object sender, RoutedEventArgs e)
        //성능 테스트 버튼 클릭 이벤트
        {
            
        }
        private void EtsButtonclk(object sender, RoutedEventArgs e)
        //확장 테스트 버튼 클릭 이벤트
        {
            
        }
        private void Image1Button_Click(object sender, RoutedEventArgs e)
        //이미지 1 버튼 클릭 이벤트
        {
            img1PathC =  GetExcelPath();

            if(img1PathC != "") //이미지를 누르고 사진을 선택 안했을 때를 위한 비교
            {
                imgPic1.Source = new BitmapImage(new Uri(img1PathC));
                img1name.Text = img1PathC;
                isinit = "ON";
                ChangeInitStatus();
            }
        }
        private void Image2Button_Click(object sender, RoutedEventArgs e)
        //이미지 2 버튼 클릭 이벤트
        {
            img2PathC = GetExcelPath();

            if (img2PathC != "") //이미지를 누르고 사진을 선택 안했을 때를 위한 비교
            {
                imgPic2.Source = new BitmapImage(new Uri(img2PathC));
                img2name.Text = img2PathC;
                isinit = "ON";
                ChangeInitStatus();
            }
        }

    
        public string GetExcelPath()
            //윈도우 파일 경로 가져오는 메소드
        {
            string res = "";
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();//파일 다이얼로그 열기.
            
            //파일 다이얼로그 디폴트 확장자 필터
            dlg.DefaultExt = ".jpg";
            dlg.Filter = "JPG Files (*.jpg)|*.jpg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";
            dlg.InitialDirectory = imgDir;//다이얼로그가 처음 참조하는 경로
            
            Nullable<bool> result = dlg.ShowDialog();
            
            if (result == true)
            {
                return dlg.FileName;
            }
            return res;
        }

        private void ModeCBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
            //콤보박스 모드 변경 이벤트
        {
            mode = modeCBox.SelectedIndex;
        }
    }
}
