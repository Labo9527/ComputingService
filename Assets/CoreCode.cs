using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreCode : MonoBehaviour
{
    private bool flag = false;
    private int[, ] state = new int[3, 3];
    private int turn = 1;
    private int AI = 0;
    GUIStyle white = new GUIStyle();
    GUIStyle black = new GUIStyle();
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                state[i, j] = 0;
            }
        }

        white.normal.background = null;
        white.normal.textColor = new Color(1, 1, 1);
        white.fontSize = 100;

        black.normal.background = null;
        black.normal.textColor = new Color(0, 0, 0);
        black.fontSize = 100;
    }
    void Restart()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                state[i, j] = 0;
            }
        }
        turn = 1;
    }
    int check()
    {
        for (int i = 0; i < 3; i++)
        {
            if (state[i, 0] == state[i, 1] && state[i, 1] == state[i, 2] && state[i, 2] != 0)
            {
                return state[i, 0];
            }
        }

        for (int i = 0; i < 3; i++)
        {
            if (state[0, i] == state[1, i] && state[1, i] == state[2, i] && state[2, i] != 0)
            {
                return state[0, i];
            }
        }

        if (state[0, 0] == state[1, 1] && state[1, 1] == state[2, 2] && state[1, 1] != 0)
            return state[1, 1];

        if (state[2, 0] == state[1, 1] && state[1, 1] == state[0, 2] && state[1, 1] != 0)
            return state[1, 1];
        return 0;
    }
    bool kill(int i,int j)
    {
        state[i, j] = turn;
        if (check() != turn)
        {
            state[i, j] = 0;
            return false;
        }
        else
        {
            state[i, j] = 0;
            return true;
        }
    }
    bool defend(int i,int j)
    {
        state[i, j] = 3 - turn;
        if (check() == 3-turn)
        {
            state[i, j] = 0;
            return true;
        }
        else
        {
            state[i, j] = 0;
            return false;
        }
    }
    bool full()
    {
        bool res = true;
        for(int i = 0; i < 3; i++)
        {
            for(int j = 0; j < 3; j++)
            {
                if (state[i, j] == 0)
                    res = false;
            }
        }
        return res;
    }
    void OnGUI()
    {
        for(int i = 0; i < 3; i++)
        {
            for(int j = 0; j < 3; j++)
            {
                if (state[i, j] == 0)
                {
                    if (GUI.Button(new Rect(i * 100, j * 100, 100, 100), ""))
                    {
                        if (check() != 0)
                            break;
                        state[i, j] = turn;
                        turn = 3 - turn;
                        if (AI == 1)
                        {
                            int done = 0;
                            for(int k = 0; k < 3; k++)
                            {
                                for(int p = 0; p < 3; p++)
                                {
                                    if (state[k, p] == 0)
                                    {
                                        if (kill(k, p))
                                        {
                                            state[k, p] = turn;
                                            done = 1;
                                        }
                                        else if (defend(k, p))
                                        {
                                            state[k, p] = turn;
                                            done = 1;
                                        }
                                    }
                                    if (done == 1)
                                        break;
                                }
                                if (done == 1)
                                    break;
                            }
                            if (done == 0)
                            {
                                for(int k = 0; k < 3; k++)
                                {
                                    for(int p = 0; p < 3; p++)
                                    {
                                        if (state[k, p] == 0)
                                        {
                                            state[k, p] = turn;
                                            done = 1;
                                            break;
                                        }
                                    }
                                    if (done == 1)
                                        break;
                                }
                            }
                            turn = 3 - turn;
                        }
                    }
                }
                else if (state[i, j] == 1)
                    GUI.Button(new Rect(i * 100, j * 100, 100, 100), "O", white);
                else if (state[i, j] == 2) 
                    GUI.Button(new Rect(i * 100, j * 100, 100, 100), "X", black);
            }
        }
        if(check()==1)
            GUI.Label(new Rect(150, 300, 200, 200), "Player1 Win");
        else if(check()==2)
            GUI.Label(new Rect(150, 300, 200, 200), "Player2 Win");
        if (GUI.Button(new Rect(350, 200, 100, 100), "Restart"))
            Restart();
        if (AI == 0)
        {
            if (GUI.Button(new Rect(450, 200, 100, 100), "AI ON"))
            {
                AI = 1;
            }
        }
        else
        {
            if (GUI.Button(new Rect(450, 200, 100, 100), "AI OFF"))
            {
                AI = 0;
            }
        }
        if (full())
        {
            GUI.Label(new Rect(150, 300, 200, 200), "Tied");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
