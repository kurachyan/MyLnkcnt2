using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LRSkip;


namespace Lnkcnt
{
    public class CS_Lnkcnt
    {
        #region 共有領域
        // '16.01.13 両側余白情報削除の追加　及び、右側・左側余白処理のコメント化
        CS_LRskip lrskip;           // 両側余白情報を削除

        private static String _wbuf;       // ソース情報
        private static Boolean _empty;     // ソース情報有無
        private static int _lnkcnt;        // ネスト情報
        public String Wbuf
        {
            get
            {
                return (_wbuf);
            }
            set
            {
                _wbuf = value;
                if (_wbuf == null)
                {   // 設定情報は無し？
                    _empty = true;
                }
                else
                {   // 整形処理を行う
                    // 不要情報削除
                    if (lrskip == null)
                    {   // 未定義？
                        lrskip = new CS_LRskip();
                    }
                    lrskip.Exec(_wbuf);
                    _wbuf = lrskip.Wbuf;

                    // 作業の為の下処理
                    if (_wbuf.Length == 0 || _wbuf == null)
                    {   // バッファー情報無し
                        // _wbuf = null;
                        _empty = true;
                    }
                    else
                    {
                        _empty = false;
                    }
                }
            }
        }
        public int Lnkcnt
        {
            get
            {
                return (_lnkcnt);
            }

            set
            {
                _lnkcnt = value;
            }
        }
        #endregion

        #region コンストラクタ
        public CS_Lnkcnt()
        {   // コンストラクタ
            _wbuf = null;       // 設定情報無し
            _empty = true;
            _lnkcnt = 0;

            lrskip = null;
        }
        #endregion

        #region モジュール
        public void Clear()
        {   // 作業領域の初期化
            _wbuf = null;       // 設定情報無し
            _empty = true;
            _lnkcnt = 0;

            lrskip = null;
        }
        public void Exec()
        {   // 中カッコ（”｛”、”｝”）のネスト情報を取り出す
            if (!_empty)
            {   // バッファーに実装有り
                int _pos = 0;       // 位置情報
                char[] arry = new char[_wbuf.Length];

                arry = _wbuf.ToCharArray();
                for (_pos = 0; _pos < _wbuf.Length; _pos++)
                {
                    if (arry[_pos] == '{')
                    {   // [｛]有り？
                        _lnkcnt++;
                    }
                    else
                    {
                        if (arry[_pos] == '}')
                        {   // [｝]有り？
                            --_lnkcnt;
                        }
                    }
                }
            }
        }

        public void Exec(String msg)
        {   // 中カッコ（”｛”、”｝”）のネスト情報を取り出す
            Setbuf(msg);                 // 入力内容の作業領域設定

            if (!_empty)
            {   // バッファーに実装有り
                int _pos = 0;       // 位置情報
                char[] arry = new char[_wbuf.Length];

                arry = _wbuf.ToCharArray();
                for (_pos = 0; _pos < _wbuf.Length; _pos++)
                {
                    if (arry[_pos] == '{')
                    {   // [｛]有り？
                        _lnkcnt++;
                    }
                    else
                    {
                        if (arry[_pos] == '}')
                        {   // [｝]有り？
                            --_lnkcnt;
                        }
                    }
                }
            }
        }

        private void Setbuf(String _strbuf)
        {   // [_wbuf]情報設定
            _wbuf = _strbuf;
            if (_wbuf == null)
            {   // 設定情報は無し？
                _empty = true;
            }
            else
            {   // 整形処理を行う
                // 不要情報削除
                if (lrskip == null)
                {   // 未定義？
                    lrskip = new CS_LRskip();
                }
                lrskip.Exec(_wbuf);
                _wbuf = lrskip.Wbuf;

                // 作業の為の下処理
                if (_wbuf.Length == 0 || _wbuf == null)
                {   // バッファー情報無し
                    // _wbuf = null;
                    _empty = true;
                }
                else
                {
                    _empty = false;
                }
            }
        }
        #endregion
    }
}
