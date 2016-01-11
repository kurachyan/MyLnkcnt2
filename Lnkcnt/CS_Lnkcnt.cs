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
        CS_Rskip rskip;             // 右側余白情報を削除
        CS_Lskip lskip;             // 左側余白情報を削除

        private String _wbuf;       // ソース情報
        private Boolean _empty;     // ソース情報有無
        private int _lnkcnt;        // ネスト情報
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
                    if (rskip == null || lskip == null)
                    {   // 未定義？
                        rskip = new CS_Rskip();
                        lskip = new CS_Lskip();
                    }
                    rskip.Wbuf = _wbuf;
                    rskip.Exec();
                    lskip.Wbuf = rskip.Wbuf;
                    lskip.Exec();
                    _wbuf = lskip.Wbuf;

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
        }
        #endregion

        #region モジュール
        public void Clear()
        {   // 作業領域の初期化
            _wbuf = null;       // 設定情報無し
            _empty = true;
            _lnkcnt = 0;
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
                if (rskip == null || lskip == null)
                {   // 未定義？
                    rskip = new CS_Rskip();
                    lskip = new CS_Lskip();
                }
                rskip.Wbuf = _wbuf;
                rskip.Exec();
                lskip.Wbuf = rskip.Wbuf;
                lskip.Exec();
                _wbuf = lskip.Wbuf;

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
