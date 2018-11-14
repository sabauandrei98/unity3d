#include <iostream>
#include <cstring>
#include <vector>
#include <algorithm>
#include <fstream>
#include <map>
using namespace std;

ifstream f1("cuvinte1eng.txt");
ifstream f2("cuvinte2eng.txt");
ifstream f3("cuvinte3eng.txt");
ofstream g1("cuvinte1eng.out");
ofstream g2("cuvinte2eng.out");
ofstream g3("cuvinte3eng.out");

map<string,int> frecvente;
vector<string> cuvinte;
char c = '"';

int main() {

    int mode = 1;


    if (mode == 1)
    {
        g1<<"string[] easyEng = { ";

            while(!f1.eof())

        {
                string s;
                getline(f1,s);

                int cuv = 1;

                bool wrong = false;

                string cuvFinal = "";

                int sz = s.size();
                if (s[sz-1] == '!' or s[sz-1] == '.' or s[sz-1] == '?')
                    sz --;

                for(int i = 0; i < sz; i++)
                    {
                        if (islower(s[i]))
                        s[i] = toupper(s[i]);

                        if (s[i] == ' ')
                            cuv++;

                        if (s[i] == ',')
                                continue;

                        if (isalpha(s[i]))
                            cuvFinal += s[i];
                        else
                            wrong = true;

                    }

                if (!wrong and cuvFinal.size() > 3)
                {
                    cout<<cuvFinal<<endl;
                    frecvente[cuvFinal]++;
                    if (frecvente[cuvFinal] == 1)
                        cuvinte.push_back(cuvFinal);
                }

        }
    }
    if (mode == 2)
    {
        g2<<"string[] mediumEng = { ";
            while(!f2.eof())
        {
                string s;
                getline(f2,s);

                for(int i = 0; i < s.size(); i++)
                    if (islower(s[i]))
                        s[i] = toupper(s[i]);

                frecvente[s]++;
                if (frecvente[s] == 1)
                    cuvinte.push_back(s);
        }
    }
    if (mode == 3)
    {
        g3<<"string[] masterEng = { ";

            while(!f3.eof())
        {
                string s;
                getline(f3,s);

                int cuv = 1;

                bool wrong = false;

                string cuvFinal = "";

                int sz = s.size();
                if (s[sz-1] == '!' or s[sz-1] == '.' or s[sz-1] == '?')
                    sz --;

                for(int i = 0; i < sz; i++)
                    {
                        if (islower(s[i]))
                        s[i] = toupper(s[i]);

                        if (s[i] == ' ')
                            cuv++;

                        if (s[i] == ',')
                                continue;

                        if (s[i] != '-' and s[i] != '(' and s[i] != ':' and s[i] != ')' and s[i] != '[' and s[i] != ']')
                            cuvFinal += s[i];
                        else
                            wrong = true;

                    }

                if (!wrong and cuv >= 3 and cuv <= 8)
                {
                    cout<<cuvFinal<<endl;
                    frecvente[cuvFinal]++;
                    if (frecvente[cuvFinal] == 1)
                        cuvinte.push_back(cuvFinal);
                }

        }
    }



    sort(cuvinte.begin(), cuvinte.end());

    if (mode == 1)
    {
        for(int i = 0; i < cuvinte.size(); i++)
            if (i != cuvinte.size() - 1)
                g1<<c<<cuvinte[i]<<c<<", ";
            else
                g1<<c<<cuvinte[i]<<c;

    g1<<'\n'<<"};"<<endl;
    }

    if (mode == 2)
    {
        for(int i = 0; i < cuvinte.size(); i++)
            if (i != cuvinte.size() - 1)
                g2<<c<<cuvinte[i]<<c<<", ";
            else
                g2<<c<<cuvinte[i]<<c;

    g2<<'\n'<<"};"<<endl;
    }

    if (mode == 3)
    {
        for(int i = 0; i < cuvinte.size(); i++)
            if (i != cuvinte.size() - 1)
                g3<<c<<cuvinte[i]<<c<<", ";
            else
                g3<<c<<cuvinte[i]<<c;

    g3<<'\n'<<"};"<<endl;
    }



    return 0;
}
